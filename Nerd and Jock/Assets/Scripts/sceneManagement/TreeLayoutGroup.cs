using UnityEngine;

public class TreeLayoutGroup : MonoBehaviour
{
    public float verticalSpacing = 150f;
    public float horizontalSpacing = 150f;

    void Start() {
        ArrangeTree();
    }

    void ArrangeTree() {
        if (transform.childCount == 0) return;

        ArrangeNode(transform.GetChild(0),Vector2.zero);
    }

    void ArrangeNode(Transform node, Vector2 position) {
        RectTransform rectTransform = node.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;

        int childCount = node.childCount;
        float width = (childCount - 1) * horizontalSpacing;

        for (int i = 0 ; i < childCount; i++) {
            Vector2 childPos = new Vector2(position.x - width / 2 + i * horizontalSpacing, position.y - verticalSpacing);
            ArrangeNode(node.GetChild(i), childPos);
        }
    }
}