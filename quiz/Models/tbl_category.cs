//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quiz.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_category
    {
        public tbl_category()
        {
            this.TBL_QUESTIONS = new HashSet<TBL_QUESTIONS>();
        }
    
        public int cat_id { get; set; }
        public string cat_name { get; set; }
        public Nullable<int> cat_fk_adid { get; set; }
        public string cat_encryptedstring { get; set; }
    
        public virtual TBL_ADMIN TBL_ADMIN { get; set; }
        public virtual ICollection<TBL_QUESTIONS> TBL_QUESTIONS { get; set; }
    }
}
