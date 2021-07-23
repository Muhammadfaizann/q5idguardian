using System;
namespace q5id.guardian.Models
{
    public class Intro
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Intro(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
