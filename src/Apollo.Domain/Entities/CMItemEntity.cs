namespace Apollo.Domain.Entities
{
    public class CMItemEntity : BaseEntity
    {
        
        public required string Name { get; set; }

        public required string ItemType { get; set; }

        public decimal Status { get; set; }

        public required string Priority { get; set; }

        public required bool Publish { get; set; }

        public required string RequestedBy { get; set; }

        public string Module { get; set; }

        public DateTime TargetDate { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string ChangeBy { get; set; }
        public DateTime ChangeOn { get; set; } 

    }


}
