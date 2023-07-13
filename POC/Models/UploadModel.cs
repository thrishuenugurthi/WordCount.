namespace POC.Models
{
    public class UploadModel
    {
        public UploadModel(string words, string count)
        {
            this.Words = words;
            this.Count = count;
        }
        public string Words { get; set; }

        public string Count { get; set; }
    }
}
