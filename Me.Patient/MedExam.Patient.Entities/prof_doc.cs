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
    
    public partial class prof_doc
    {
        public int num_prof { get; set; }
        public int num_doc { get; set; }
        public string name_doc { get; set; }
    
        public virtual doctor doctor { get; set; }
        public virtual profess profess { get; set; }
    }
}
