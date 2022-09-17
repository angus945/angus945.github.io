namespace DataDriven.TextProcess
{
    public struct ProcessingData
    {
        public string dataName;
        public string dataFlag;

        public string[] contents;
        public string[] contentFlags;

        public ProcessingData(string name, string flag, string[] contents, string[] contentFlags) : this()
        {
            this.dataName = name;
            this.dataFlag = flag;
            this.contents = contents;
            this.contentFlags = contentFlags;
        }
        public ProcessingData(string name, string flag, string content, string contentFlag) : this()
        {
            this.dataName = name;
            this.dataFlag = flag;
            this.contents = new string[] { content };
            this.contentFlags = new string[] { contentFlag };
        }

        public override string ToString()
        {
            return $"tag: {dataName}, flags: {contentFlags.PrintOut()}, datas: {contents.PrintOut()}";
        }
    }
}
