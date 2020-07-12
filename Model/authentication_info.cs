namespace QuickFactorService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class authentication_info
    {
        public decimal? RECORD_UID { get; set; }

        [Key]
        public string NUID { get; set; }

        [StringLength(128)]
        public string PIN { get; set; }

        [StringLength(50)]
        public string FIRST_NAME { get; set; }

        [StringLength(50)]
        public string MIDDLE_NAME { get; set; }

        [StringLength(50)]
        public string LAST_NAME { get; set; }

        [StringLength(240)]
        public string Full_Name { get; set; }

        [StringLength(50)]
        public string QUESTION { get; set; }

        [StringLength(50)]
        public string REGION { get; set; }

        [StringLength(50)]
        public string TARGET_RESET_SYSTEM { get; set; }

        [StringLength(20)]
        public string ID1 { get; set; }

        [StringLength(20)]
        public string ID2 { get; set; }

        [StringLength(20)]
        public string ID3 { get; set; }

        [StringLength(20)]
        public string ID5 { get; set; }

        [StringLength(20)]
        public string ID6 { get; set; }

        [StringLength(50)]
        public string ANSWER { get; set; }

        [StringLength(255)]
        public string EMAIL_ID { get; set; }

        [StringLength(50)]
        public string Added_By { get; set; }

        [StringLength(8)]
        public string Added_By_Date { get; set; }

        [StringLength(8)]
        public string Added_Date { get; set; }

        [StringLength(20)]
        public string Reg_By { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(20)]
        public string REG_PIN { get; set; }

        [StringLength(50)]
        public string SS_Question1 { get; set; }

        [StringLength(50)]
        public string SS_Answer1 { get; set; }

        [StringLength(50)]
        public string SS_Question2 { get; set; }

        [StringLength(50)]
        public string SS_Answer2 { get; set; }

        [StringLength(50)]
        public string SS_Question3 { get; set; }

        [StringLength(50)]
        public string SS_Answer3 { get; set; }

        [StringLength(50)]
        public string HD_Question { get; set; }

        [StringLength(50)]
        public string HD_Answer { get; set; }

        [StringLength(50)]
        public string IVR_Question { get; set; }

        [StringLength(128)]
        public string IVR_Answer { get; set; }

        public DateTime? PIN_GenDate { get; set; }

        public DateTime? STATUS_CHG { get; set; }

        public DateTime? PEDATETIME { get; set; }

        public int? USE_FOR_LAB { get; set; }

        public int? TEST_CAN_DELETE { get; set; }
    }
}
