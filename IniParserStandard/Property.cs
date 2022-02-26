using IniParser.Model;

namespace IniParser
{
    public class Property
    {
        public string Section { get; }

        public string Key { get; }

        public string Comment { get; }

        private readonly IniData _data;

        public Property(IniData data, string section, string key, string comment)
        {
            _data = data;
            Section = section;
            Key = key;
            Comment = comment;
        }

        public string Value
        {
            get => _data[Section][Key];
            set => _data[Section][Key] = value;
        }
    }
}

