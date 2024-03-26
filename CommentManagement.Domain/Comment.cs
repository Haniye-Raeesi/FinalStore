using _0_FrameWork.Domain;
using System;

namespace CommentManagement.Domain
{
        public class Comment : EntityBase
        {

            public string Name { get; private set; }
            public string Email { get; private set; }
            public string Message { get; private set; }
            public bool IsCanceled { get; private set; }
            public bool IsConfirmed { get; private set; }
            public DateTime CommentDate { get; private set; }
            public string WebSite { get; private set; }
            public long OwnerRecordId { get; private set; }
            public int Type { get; private set; }
            public long ParentId { get; private set; }
            public Comment ParentComment { get; private set; }

        public Comment() 
        { 
        }
        public Comment(string name, string email, string message
           , string webSite, long ownerRecordId, int type, long parentId)
        {
            Name = name;
            Email = email;
            Message = message;
            WebSite = webSite;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
            CommentDate = DateTime.Now;
        }

        public void Cancel() 
        {
            IsCanceled = true;
        }
        public void Confirm() 
        {
            IsConfirmed = true;
        }
    }
    
}
