using System;
using System.Collections.Generic; 

namespace YouTubeVideoApp
{
    public class Comment
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Comment(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Name}: {Text}";
        }
    }

    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }
        private List<Comment> Comments { get; set; } 

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        public List<Comment> GetComments()
        {
            return Comments;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Video> videos = new List<Video>();

            // Create first video with comments
            Video video1 = new Video("Video Title 1", "Author 1", 120);
            video1.AddComment(new Comment("User 1", "Comment 1"));
            video1.AddComment(new Comment("User 2", "Comment 2"));
            videos.Add(video1);

            // Create second video with comments
            Video video2 = new Video("Video Title 2", "Author 2", 300);
            video2.AddComment(new Comment("User 3", "Comment 3"));
            video2.AddComment(new Comment("User 4", "Comment 4"));
            video2.AddComment(new Comment("User 5", "Comment 5"));
            videos.Add(video2);

            // Display video information
            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Title}, Author: {video.Author}, Length: {video.Length} seconds, Comments: {video.GetNumberOfComments()}");
                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine(comment.ToString());
                }
            }
        }
    }
}

