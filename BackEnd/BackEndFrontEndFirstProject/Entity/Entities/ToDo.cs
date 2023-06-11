using BackEndFrontEndFirstProject.Core;

namespace BackEndFrontEndFirstProject.Entity.Entities
{
    public class ToDo : EntityBase
    {
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
