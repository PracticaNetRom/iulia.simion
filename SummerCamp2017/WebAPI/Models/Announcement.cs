//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Announcement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Announcement()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int AnnouncementId { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public System.DateTime PostDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public int CategoryId { get; set; }
        public bool Closed { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}