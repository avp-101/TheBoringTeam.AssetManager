using System;
using System.Collections.Generic;
using System.Text;

namespace TheBoringTeam.AssetManagement.Models.Infrastructure
{
    public class ImageRecognitionResult
    {
        public DescriptionResult description { get; set; }
        public IEnumerable<CategoriesResult> categories { get; set; }
    }

    public class CategoriesResult
    {
        public string name { get; set; }
        public float score { get; set; }
    }

    public class DescriptionResult
    {
        public string[] tags { get; set; }
        public CaptionsResult[] captions { get; set; }
    }

    public class CaptionsResult
    {
        public string text { get; set; }
        public float confidence { get; set; }
    }
}
