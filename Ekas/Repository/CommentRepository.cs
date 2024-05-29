using Ekas.Monitoring.Models;

namespace Ekas.Monitoring.Repository
{
    public class CommentRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CommentRepository(ApplicationDbContext applicationContext)
        {
            this.applicationDbContext = applicationContext;
        }

        public string GetCommentTextByIncidentId(int incidentId)
        {
            var comment = applicationDbContext.Comments.Where(i => i.IncidentId == incidentId).FirstOrDefault();
            return comment?.Text;
        }

        public void AddComment(Comment comment)
        {
            applicationDbContext.Comments.Add(comment);
             applicationDbContext.SaveChanges();
        }

        public void UpdateCommentText(string commentText, int id)
        {
            var comment = applicationDbContext.Comments.Where(i => i.IncidentId == id).FirstOrDefault();
            comment.Text = commentText;
            applicationDbContext.Comments.Update(comment);
            applicationDbContext.SaveChanges();
        }
    }
}
