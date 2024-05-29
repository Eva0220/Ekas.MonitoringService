using Ekas.Monitoring.Models;
using Ekas.Monitoring.Repository;

namespace Ekas.Monitoring.Services
{
    public class CommentService
    {
        private readonly CommentRepository commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        public void AddComment(string commentText, Incident incident)
        {
            var comment = new Comment()
            {
                Text = commentText,
                IncidentId = incident.Id
            };
            commentRepository.AddComment(comment);
        }

        public string GetText(int incidentId)
        {
            return commentRepository.GetCommentTextByIncidentId(incidentId);
        }

        public void UpdateComment(string commentText, int incidentId)
        {
            commentRepository.UpdateCommentText(commentText, incidentId);
        }
    }
}
