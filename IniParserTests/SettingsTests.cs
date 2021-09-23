using Xunit;
using IniParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IniParser.Tests
{
    public class SettingsTests
    {
        [Fact()]
        public void SettingsTest()
        {
            _ = Assert.Throws<FileNotFoundException>(() =>
              {
                  _ = new Settings("NotFoundSettings.ini");
              });
            
            _ = Assert.Throws<ArgumentException>(() =>
              {
                  _= new Settings("Settings.ini2");
              });

            var fileName = "Settings.ini";
            var settings = new Settings(fileName);
            Assert.True(fileName == settings.FileName);
        }
    }
}