//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyofPlants
{
    using System;
    using System.Collections.Generic;
    
    public partial class Services
    {
        public int Id { get; set; }
        public string Type_service { get; set; }
        public string Who_make_service { get; set; }
        public int Id_staff { get; set; }
    
        public virtual Workers Workers { get; set; }
    }
}
