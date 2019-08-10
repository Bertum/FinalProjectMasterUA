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

    public DialogBuilder WithTextId(string textId)
    {
        data.TextId = textId;
        return this;
    }

    public DialogBuilder WithImage(Image image)
    {
        data.Image = image;
        return this;
    }

    public DialogBuilder WithCharacterName(string name)
    {
        data.CharacterName = name;
        return this;
    }

    public DialogBuilder IsQuest()
    {
        data.IsQuest = true;
        return this;
    }

    public DialogBuilder WithCharacter(string name, Image image)
    {
        data.CharacterName = name;
        data.Image = image;
        return this;
    }
}
