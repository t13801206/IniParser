using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IniParser.Model;

namespace IniParser
{
    public class Settings
    {
        public string FileName { get; }

        public ICollection<Property> PropertyCollection { get; } = new List<Property>();

        private readonly FileIniDataParser _parser = new FileIniDataParser();
        private readonly IniData _data;

        public Settings(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File Not Found {Path.GetFullPath(fileName)}");
            }

            if (Path.GetExtension(fileName) != ".ini")
            {
                throw new ArgumentException($"Invalid Extension {fileName}");
            }

            FileName = fileName;
            _data = _parser.ReadFile(fileName);

            SetPropertyCollection(_data);
        }

        public string this[string section, string key]
        {
            get => _data[section][key];
            set => _data[section][key] = value;
        }

        private void SetPropertyCollection(IniData data)
        {
            foreach (var section in data.Sections)
            {
                section.Comments.ForEach(x => Debug.WriteLine(x));

                foreach (var key in section.Keys)
                {
                    string comment = "";
                    key.Comments.ForEach(x =>
                    {
                        Debug.WriteLine(x);
                        comment += x + '\n';
                    });
                    comment = comment.TrimEnd('\n');

                    Debug.WriteLine($"{section.SectionName}\t{key.KeyName}\t{key.Value}\t");

                    var property = new Property(_data, section.SectionName, key.KeyName, comment);
                    PropertyCollection.Add(property);
                }
            }
        }

        public void Save() => _parser.WriteFile(FileName, _data);
    }
}
