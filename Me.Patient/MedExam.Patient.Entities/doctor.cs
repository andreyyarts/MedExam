//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedExam.Patient.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class doctor
    {
        public doctor()
        {
            this.doc_fio_doc = new HashSet<doc_fio_doc>();
            this.fac_doc = new HashSet<fac_doc>();
            this.mar_list_doc = new HashSet<mar_list_doc>();
            this.ob_doctor = new HashSet<ob_doctor>();
            this.pac_doc = new HashSet<pac_doc>();
            this.priem = new HashSet<priem>();
            this.prof_doc = new HashSet<prof_doc>();
        }
    
        public int num_doc { get; set; }
        public string name_doc { get; set; }
        public string kabinet { get; set; }
        public string kod_preys { get; set; }
        public Nullable<double> stavka { get; set; }
    
        public virtual ICollection<doc_fio_doc> doc_fio_doc { get; set; }
        public virtual ICollection<fac_doc> fac_doc { get; set; }
        public virtual ICollection<mar_list_doc> mar_list_doc { get; set; }
        public virtual ICollection<ob_doctor> ob_doctor { get; set; }
        public virtual ICollection<pac_doc> pac_doc { get; set; }
        public virtual ICollection<priem> priem { get; set; }
        public virtual ICollection<prof_doc> prof_doc { get; set; }
    }
}
