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
    
    public partial class priem_an
    {
        public int tab_num { get; set; }
        public string num_an { get; set; }
        public int num_pac { get; set; }
        public string fam_pac { get; set; }
        public System.DateTime date_priem { get; set; }
        public Nullable<int> num_m_s { get; set; }
        public string dop { get; set; }
    
        public virtual analiz analiz { get; set; }
        public virtual fio_doctor fio_doctor { get; set; }
        public virtual pacient pacient { get; set; }
    }
}
