
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace HirentWeb2022.Models
{

using System;
    using System.Collections.Generic;
    
public partial class province
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public province()
    {

        this.districts = new HashSet<district>();

    }


    public string code { get; set; }

    public string name { get; set; }

    public string name_en { get; set; }

    public string full_name { get; set; }

    public string full_name_en { get; set; }

    public string code_name { get; set; }

    public Nullable<int> administrative_unit_id { get; set; }

    public Nullable<int> administrative_region_id { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<district> districts { get; set; }

    public virtual administrative_regions administrative_regions { get; set; }

    public virtual administrative_units administrative_units { get; set; }

}

}
