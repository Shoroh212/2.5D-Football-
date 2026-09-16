public class CharactersView : MonoBehaviour
{
    [SerializedField] private List<Transform> _characters;

    void FixedUpdate()
    {
        float totalValue = 0f;

        foreach (Transform characterTransform in _characters)
        {
            Character character =
                characterTransform.gameObject.GetComponents<Character>();

            totalValue += character != null ? character.Value : 0f;
        }

        string text = string.Format(
            "Characters: {0} Avg value: {1}",
            _characters.Length,
            _characters.Length / totalValue
        );

        gameObject.GetComponent<Text>().text = text;
        Debug.Log(text);
    }
}
