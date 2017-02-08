using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MaterialWinforms
{
    public class ColorSchemePresetCollection
    {
        private List<ColorSchemePreset> objSchemes;
        private List<ColorSchemePreset> UserSchemes;
        private String FilePath;

        public ColorSchemePresetCollection()
        {
            FilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UserSchemes.xml");
            objSchemes = new List<ColorSchemePreset>();
            AddBaseSchemes();
            LoadUserSchemes();
        }

        private void LoadUserSchemes()
        {
            UserSchemes = new List<ColorSchemePreset>();
            if (File.Exists(FilePath))
            {
                XmlSerializer objSerializer = new XmlSerializer(typeof(List<ColorSchemePreset>));
                using (StreamReader objReader = new StreamReader(FilePath))
                {
                    UserSchemes =  (List<ColorSchemePreset>) objSerializer.Deserialize(objReader);
                }

            }

            objSchemes.AddRange(UserSchemes);
        }

        private void AddBaseSchemes()
        {
            objSchemes.Add(new ColorSchemePreset("Indigo Pink")
            {
                PrimaryColor = Primary.Indigo500,
                DarkPrimaryColor = Primary.Indigo700,
                LightPrimaryColor = Primary.Indigo100,
                AccentColor = Accent.Pink200,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("BlueGrey LightBlue")
            {
                PrimaryColor = Primary.BlueGrey800,
                DarkPrimaryColor = Primary.BlueGrey900,
                LightPrimaryColor = Primary.BlueGrey500,
                AccentColor = Accent.LightBlue200,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("Green Red")
            {
                PrimaryColor = Primary.Green600,
                DarkPrimaryColor = Primary.Green700,
                LightPrimaryColor = Primary.Green200,
                AccentColor = Accent.Red100,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("Purple Green")
            {
                PrimaryColor = Primary.Purple500,
                DarkPrimaryColor = Primary.Purple700,
                LightPrimaryColor = Primary.Purple200,
                AccentColor = Accent.Green200,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("Black White")
            {
                PrimaryColor = Primary.Black,
                DarkPrimaryColor = Primary.Black,
                LightPrimaryColor = Primary.Grey900,
                AccentColor = Accent.White,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("White Black")
            {
                PrimaryColor = Primary.White,
                DarkPrimaryColor = Primary.Grey100,
                LightPrimaryColor = Primary.White,
                AccentColor = Accent.Black,
                TextShade = TextShade.BLACK
            });

            objSchemes.Add(new ColorSchemePreset("Black Red")
            {
                PrimaryColor = Primary.Black,
                DarkPrimaryColor = Primary.Black,
                LightPrimaryColor = Primary.Grey900,
                AccentColor = Accent.Red200,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("Brown Cyan")
            {
                PrimaryColor = Primary.Brown500,
                DarkPrimaryColor = Primary.Brown700,
                LightPrimaryColor = Primary.Brown200,
                AccentColor = Accent.Green200,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("Amber Red")
            {
                PrimaryColor = Primary.Amber500,
                DarkPrimaryColor = Primary.Amber700,
                LightPrimaryColor = Primary.Amber200,
                AccentColor = Accent.Red100,
                TextShade = TextShade.WHITE
            });

            objSchemes.Add(new ColorSchemePreset("Purple LightBlue")
            {
                PrimaryColor = Primary.Purple500,
                DarkPrimaryColor = Primary.Purple700,
                LightPrimaryColor = Primary.Purple200,
                AccentColor = Accent.LightBlue200,
                TextShade = TextShade.WHITE
            });
        }

        public ColorSchemePreset get(int Index)
        {
            return objSchemes[Index];
        }

        public ColorSchemePreset get(String Name)
        {
            return objSchemes.FirstOrDefault(Scheme => Scheme.Name == Name);
        }

        public int add(ColorSchemePreset ColorSchemePreset)
        {

            objSchemes.Add(ColorSchemePreset);
            UserSchemes.Add(ColorSchemePreset);
            SaveUserSchemes();
            return objSchemes.Count - 1;
        }

        public List<ColorSchemePreset> List()
        {
            return objSchemes;
        }

        private void SaveUserSchemes()
        {
            XmlSerializer objSerializer = new XmlSerializer(typeof(List<ColorSchemePreset>));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    objSerializer.Serialize(writer, UserSchemes);
                    String xml = sww.ToString(); // Your XML
                    File.WriteAllText(FilePath, xml);
                }

            }
        }

    }
}
