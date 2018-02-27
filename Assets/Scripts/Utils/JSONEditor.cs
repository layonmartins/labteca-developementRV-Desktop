using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

/*public static class JSONEdit {
	//! Reads the file
	//  If the file doesn't exists, creates the file
	public static JSONNode Read(string path) {
		if(!System.IO.File.Exists(path)) {
			System.IO.File.Create(path);
			return Read(path);
		}
		else {
			System.IO.StreamReader sr = new System.IO.StreamReader (path);
			string content = sr.ReadToEnd ();
			sr.Close();
			
			return JSON.Parse (content);
		}
	}
	public static void Write(string path) {
	}
}*/

//! Loads a .json file.
//  The class makes use of SimpleJSON namespace to handle JSON I/O
public class JSONEditor {
	
	private JSONNode json;
	
	public JSONEditor (string file) {
		
		#if UNITY_WEBPLAYER
		json = ReadFromWeb(file);
		#else
		json = Read (file);
		#endif
	}

	public JSONEditor (string file, bool difference) {
		json = ReadFromResources (file);
	}

	//! Reads the file
	//  If the file doesn't exists, creates the file
	private JSONNode Read(string file) {

        //A versão Android SDK não estava carregando o banco de dados, então de eu substitui aqui a leitura do json inserindo diretamente a string. Funcionou Aleluia.

        Debug.Log("File -> " + file);
        if (file == "journalItems")
        {
            string content = "{\"name\" : \"Análise da Solução\",\"objects\": [{\"index\" : \"0\",\"name\" : \"Utilize o mapa para descobrir em qual sala está o pHmetro e faça a medição do produto (NaCL) que esta no seu iventário.\",\"isDone\" : \"False\",\"numberOfPrerequisites\" : \"0\"},{	\"index\" : \"1\",\"name\" : \"Com o auxílio dos equipamentos, descubra qual é o composto presente na solução.\",\"isDone\" : \"False\",\"numberOfPrerequisites\" : \"1\",\"indexPrerequisiteOf\" : [\"0\"]},{\"index\" : \"2\",\"name\" : \"Calcule a concentração percentual da amostra, em gramas por 100ml.\",\"isDone\" : \"False\",\"numberOfPrerequisites\" : \"1\",\"indexPrerequisiteOf\" : [\"1\"]}]}";
            return JSON.Parse(content);
        }

        else if (file == "products")
        {
            string content = "{\"objects\":[{\"name\":\"Cloreto de Sódio\", \"formula\":\"NaCl\", \"isSolid\": \"True\", \"molarMass\": \"58.5\", \"purity\": \"1\", \"density\": \"1\",\"polarizability\": \"0\", \"irSpecter\": \"\", \"conductibility\": \"274.6\",	\"solubility\": \"1\",	\"fumeHoodOnly\": \"false\",\"compoundColorR\": \"255\", \"compoundColorG\": \"255\",\"compoundColorB\": \"255\" }]}";
            return JSON.Parse(content);
        }
        else if( file == "customPhase")
        {
            string content = "{\"glasswareStart\" : \"true\",\"compoundFormula\" : \"NaCl\",\"volume\" : \"200.0\",\"molarity\" : \"1.0\",\"objects\" : [{\"typeOfStep\" : \"1\",\"option1\" : \"Sal\",\"option2\" : \"Base\",\"option3\" : \"Ácido\",\"option4\" : \"Orgânico\",\"option5\" : \"Etc\",\"correctAnswer\" : \"1\"},{\"typeOfStep\" : \"2\",\"compoundFormula\" : \"NaCl\"},{\"typeOfStep\" : \"3\",\"compoundFormula\" : \"NaCl\",\"molarity\" : \"1.0\",\"maxError\" : \"0.2\"}]}";
            return JSON.Parse(content);
        } else if (file == "compounds")
        {
            string content = "{\"objects\": [{\"name\": \"Ácido Clorídrico\",\"formula\": \"HCl\",\"isSolid\": \"False\",\"molarMass\": \"36.5\",\"purity\": \"0.37\",\"density\": \"1.18\",\"polarizability\": \"0\",\"compoundColorR\": \"255\",\"compoundColorG\": \"255\",\"compoundColorB\": \"255\",\"irSpecter\": \"\",\"conductibility\": \"1\",\"solubility\": \"1\",\"fumeHoodOnly\": \"True\",\"pH\": \"0\",\"turbidity\": \"0.5\",\"refratometer\": \"2\",\"flameSpecter\": \"0.5\",\"uvSpecter\": \"\",\"hplc\": \"\"}, {\"name\": \"Hidróxido de Sódio\",\"formula\": \"NaOH\",\"isSolid\": \"True\",\"molarMass\": \"40\",\"purity\": \"1\",\"density\": \"1\",\"polarizability\": \"0\",\"compoundColorR\": \"255\",\"compoundColorG\": \"255\",\"compoundColorB\": \"255\",\"irSpecter\": \"\",\"conductibility\": \"2\",\"solubility\": \"1\",\"fumeHoodOnly\": \"False\"}, {\"name\": \"Ácido Sulfúrico\",\"formula\": \"H2SO4\",\"isSolid\": \"False\",\"molarMass\": \"98\",\"purity\": \"0.65\",\"density\": \"1.84\",\"polarizability\": \"0\",\"compoundColorR\": \"255\",\"compoundColorG\": \"200\",\"compoundColorB\": \"90\",\"irSpecter\": \"\",\"conductibility\": \"2\",\"solubility\": \"1\",\"fumeHoodOnly\": \"False\",\"pH\": \"0\",\"turbidity\": \"2\",\"refratometer\": \"2\",\"flameSpecter\": \"0.5\",\"uvSpecter\": \"\",\"hplc\": \"\"}]}";
            return JSON.Parse(content);
        } else if (file == "anotacoes")
        {
            string content = "{\"quantidadeAnotacoes\":\"0\"}";
            return JSON.Parse(content);
        } else
        {

            string directory = "";

            #if UNITY_STANDALONE_WIN
			                directory = "Assets\\Resources\\" + file + ".json";
            #endif
            #if UNITY_STANDALONE_LINUX
			                directory = "Assets/Resources/" + file + ".json";
            #endif
            #if UNITY_STANDALONE_OSX
			                directory = "LabTecA.app/Contents/Data/Resources/" + file + ".json";
            #endif
            #if UNITY_ANDROID
                        char sep = Path.DirectorySeparatorChar;
                        directory = "Assets" + sep + "Resources" + sep + file + ".json";
#endif

            

            if (!System.IO.File.Exists(directory))
            {
                System.IO.File.Create(directory);
                return Read(file);
            }
            else
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(directory);
                string content = sr.ReadToEnd();
                sr.Close();


                Debug.Log("file" + file);

                return JSON.Parse(content);
            }
        }
	}

	/// <summary>
	/// Reads the files from the LibraryFromWeb class, that previously downloaded the values during game introduction.
	/// </summary>
	/// <returns>Returns .json read in form of JSONNode.</returns>
	/// <param name="file">Name of file to be loaded.</param>
	private JSONNode ReadFromWeb(string file) {
		return JSON.Parse (GameObject.Find ("LibraryFromWeb").GetComponent<LibraryFromWeb>().GetData (file));
	}

	/// <summary>
	/// Reads a file from the resources folder resources, integrated in the .exe.
	/// </summary>
	/// <param name="file">The file to be read.</param>
	private JSONNode ReadFromResources(string file) {
		//string filePath = "SetupData/" + file;
		TextAsset asset = Resources.Load(file) as TextAsset;
		return JSON.Parse (asset.text);
	}

	private void Write() {
		
	}
	
	public bool GetBool(int numObject, string data) {
		return json["objects"][numObject][data].AsBool;
	}
	
	public string GetString(int numObject, string data) {
		return json ["objects"] [numObject] [data].Value;
	}
	
	public int GetInt(int numObject, string data) {
		return json ["objects"] [numObject] [data].AsInt;
	}
	
	public float GetFloat(int numObject, string data) {
		return json ["objects"] [numObject] [data].AsFloat;
	}
	
	public int NumberOfObjects() {
		return json ["objects"].Count;
	}
	
	public int NumberOfFields(int numObject) {
		return json["objects"] [numObject].Count;
	}
	//Returns the string value of a sub-item
	public string GetSubValue(int numObject, string data, int position) {
		return json ["objects"] [numObject] [data][position].Value;
	}
	public string GetMainValue(string data) {
		return json [data].Value;
	}

	public int GetMainInt(string data) {
		return json[data].AsInt;
	}
}