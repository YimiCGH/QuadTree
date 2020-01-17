using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree : MonoBehaviour
{
    public Vector2 m_size;
    public int m_maxInNode;

    public int m_pointNums;
    public Point m_prefab;

    QuadTreeNode m_root;

    private void Start()
    {
        m_root = new QuadTreeNode(transform.position.ToPoint_XZ(), m_size);
        StartCoroutine(AddPoints());
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out float lenght))
        {
            var position = ray.GetPoint(lenght);
            Debug.DrawLine(ray.origin, position, Color.red);
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsInRootRect(new Vector2(position.x,position.z))) {
                    return;
                }
                //Create point
                var point = Instantiate(m_prefab, position, Quaternion.identity, transform);

                point.Position = new Vector2(position.x, position.z);
                AddPoint(m_root, point);
            }
        }       
    }

    public List<I_Storeable> SelectInRect(Rectangle _rect,out List<Rectangle> _intersectRects) {
        _intersectRects = new List<Rectangle>();

        List<QuadTreeNode> openList = new List<QuadTreeNode>();
        List<I_Storeable> targets = new List<I_Storeable>();

        openList.Add(m_root);

        while (openList.Count > 0) {
            var node = openList[0];
            openList.RemoveAt(0);

            if (node.Rect.IntersectWith(_rect)) {
                var children = node.Children;
                if (children != null)
                {
                    openList.AddRange(children);
                    if(node.ShareObjects != null)
                        targets.AddRange(node.ShareObjects);
                }
                else {
                    if(node.Objects != null)
                        targets.AddRange(node.Objects);
                    _intersectRects.Add(node.Rect);
                }
            }
        }

        for (int i = targets.Count - 1; i >= 0 ; i--){
            var obj = targets[i];
            if (!_rect.ContainPoint(obj.Position)) {
                targets.RemoveAt(i);
            }
        }
        return targets;
    }

    public List<I_Storeable> SelectInRange<T>(T _range,out List<Rectangle> _intersectRects) where T:I_Range
    {
        _intersectRects = new List<Rectangle>();

        List<QuadTreeNode> openList = new List<QuadTreeNode>();
        List<I_Storeable> targets = new List<I_Storeable>();

        openList.Add(m_root);

        while (openList.Count > 0)
        {
            var node = openList[0];
            openList.RemoveAt(0);

            if (_range.IntersectWith( node.Rect))
            {
                var children = node.Children;
                if (children != null)
                {
                    openList.AddRange(children);
                    if (node.ShareObjects != null)
                        targets.AddRange(node.ShareObjects);
                }
                else
                {
                    if (node.Objects != null)
                        targets.AddRange(node.Objects);
                    _intersectRects.Add(node.Rect);
                }
            }
        }

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            var obj = targets[i];
            if (!_range.ContainPoint(obj.Position))
            {
                targets.RemoveAt(i);
            }
        }
        return targets;
    }

    bool IsInRootRect(Vector2 _position) { return m_root.Rect.ContainPoint(_position); }

    IEnumerator AddPoints() {
        var rect = m_root.Rect;
        for (int i = 0; i < m_pointNums; i++)
        {
            var position = new Vector3(
                Random.Range(rect.left, rect.right),
                0,
                Random.Range(rect.bottom, rect.top));

            var point = Instantiate(m_prefab, position, Quaternion.identity,transform);
       
            point.Position = new Vector2(position.x, position.z);
            AddPoint(m_root,point);
            yield return null;
        }
    }

    bool AddPoint(QuadTreeNode _node, Point _point) {
        if (!_node.Rect.ContainPoint(_point.Position))
            return false;
        var objectList = _node.Objects;
        var children = _node.Children;
        objectList.Add(_point);

        if (children == null ){
            if (objectList.Count  <= m_maxInNode){
                return true;
            }
            else {
                children = SubDivid(_node);
            }            
        }        

        for (int i = 0; i < objectList.Count; i++){
            var point = objectList[i] as Point;
            bool isInChildNode = false;
            for (int j = 0; j < 4; j++) {
                var child = children[j];
                if (AddPoint(child, point)){
                    isInChildNode = true;
                    break;
                }
            }
            if (!isInChildNode){
                _node.ShareObjects.Add(point);
            }
        }
        objectList.Clear();

        return true;
    }

    QuadTreeNode[] SubDivid(QuadTreeNode _node) {
        var children = _node.Split();
        _node.Children = children;
        for (int i = 0; i < 4; i++)
        {
            children[i].Parent = _node;
        }
        return children;
    }

    private void OnDrawGizmos()
    {
        var node = m_root;
        if (node == null)
            return;

        var coners = node.Rect.Coners;
        Gizmos.color = Color.red;
        GizmosUtils.Draw2DLine(coners[0], coners[1]);
        GizmosUtils.Draw2DLine(coners[1], coners[2]);
        GizmosUtils.Draw2DLine(coners[2], coners[3]);
        GizmosUtils.Draw2DLine(coners[3], coners[0]);

        List<QuadTreeNode> openList = new List<QuadTreeNode>
        {
            node
        };

        List<QuadTreeNode> leafNodes = new List<QuadTreeNode>();


        while (openList.Count > 0) {
            var curnode = openList[0];
            openList.RemoveAt(0);

            var children = curnode.Children;
            if (children != null){
                openList.AddRange(children);
            }else {
                leafNodes.Add(curnode);
            }
        }
        
        Gizmos.color = Color.blue;
        for (int i = 0; i < leafNodes.Count; i++)
        {
            var curNode = leafNodes[i];
            if (curNode == m_root)
                continue;

            coners = curNode.Rect.Coners;
            GizmosUtils.Draw2DLine(coners[0], coners[1]);
            GizmosUtils.Draw2DLine(coners[1], coners[2]);
            GizmosUtils.Draw2DLine(coners[2], coners[3]);
            GizmosUtils.Draw2DLine(coners[3], coners[0]);
        }
    }
}
