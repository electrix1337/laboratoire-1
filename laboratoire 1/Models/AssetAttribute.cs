using System;

namespace JsonDAL
{
    public class AssetAttribute : Attribute
    {
        private readonly string folder;

        public AssetAttribute(string folder)
        {
            this.folder = folder;
        }
        public string Folder() => folder;
    }
}