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
    
    public partial class mar_list_an
    {
        public string num_an { get; set; }
        public string name_an { get; set; }
        public int tab_num { get; set; }
        public string fio_doc { get; set; }
        public string kabinet { get; set; }
        public string num_mar { get; set; }
    
        public virtual analiz analiz { get; set; }
        public virtual fio_doctor fio_doctor { get; set; }
    }
}
