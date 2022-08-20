using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour
{
   public enum ImportMode
    {
        FromScratch,
        FromScriptableObjects,
    }

    public List<Character> characters;
    public ImportMode importMode = ImportMode.FromScratch;

    // Start is called before the first frame update
    void Start()
    {
        if (importMode == ImportMode.FromScratch)
        {
            characters = ImportFromScratch();
        }

        if (importMode == ImportMode.FromScriptableObjects)
        {
            characters = ImportFromScriptableObjects();
        }
     }

    public Character GetCharacter(string name)
    {
        return characters.Find(
            character => character.unitName.Contains(name)
        );
    }

    public Character GetCharacter(int id)
    {
        return characters.Find(
            character => character.id.Equals(id)
        );
    }

    public List<Character> ImportFromScratch ()
    {
        List<Character> tempCharacters = new List<Character>();

        Character fulano = new Character(id: 10, unitName: "Fulano");
        tempCharacters.Add(fulano);

        return tempCharacters;
    }

    public List<Character> ImportFromScriptableObjects ()
    {
        List<CharacterSO> characterObjects = Resources.LoadAll<CharacterSO>("").ToList();

        List<Character> tempCharacters = new List<Character>();
        foreach (CharacterSO characterObject in characterObjects)
        {
            tempCharacters.Add(characterObject.character);
        }

        return tempCharacters;
    }
}