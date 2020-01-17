using UnityEngine;
using System.Collections;

public static class QuadTreeUtil
{

    public static QuadTreeNode[] Split(this QuadTreeNode _node) {
        var center = _node.Rect.m_center;
        var childsize = _node.Rect.m_size * 0.5f;

        QuadTreeNode[] childNodes = new QuadTreeNode[4];

        Rectangle temp = new Rectangle(center, childsize);
        var coners = temp.Coners;

        childNodes[0] = new QuadTreeNode(new Rectangle(coners[0],childsize));
        childNodes[1] = new QuadTreeNode(new Rectangle(coners[1],childsize));
        childNodes[2] = new QuadTreeNode(new Rectangle(coners[2], childsize));
        childNodes[3] = new QuadTreeNode(new Rectangle(coners[3], childsize));

        return childNodes;
    }
}
