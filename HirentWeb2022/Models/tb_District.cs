
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
    
public partial class tb_District
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public tb_District()
    {

        this.tb_Ward = new HashSet<tb_Ward>();

    }


    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public string LatiLongTude { get; set; }

    public int ProvinceId { get; set; }

    public Nullable<int> SortOrder { get; set; }

    public Nullable<bool> IsPublished { get; set; }

    public Nullable<bool> IsDeleted { get; set; }



    public virtual tb_Province tb_Province { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tb_Ward> tb_Ward { get; set; }

}

}
