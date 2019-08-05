using UnityEngine.UI;

public class DialogBuilder
{
    private DialogData data;

    public DialogBuilder()
    {
        data = new DialogData();
    }

    public DialogData Build()
    {
        return data;
    }

    public void WithTextId(string textId)
    {
        data.TextId = textId;
    }

    public void WithImage(Image image)
    {
        data.Image = image;
    }

    public void WithCharacterName(string name)
    {
        data.CharacterName = name;
    }

    public void IsQuest()
    {
        data.IsQuest = true;
    }

    public void WithCharacter(string name, Image image)
    {
        data.CharacterName = name;
        data.Image = image;
    }
}
