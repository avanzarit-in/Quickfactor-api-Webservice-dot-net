namespace QuickFactorService
{
    using System.Data.Entity;

    public partial class QuickFactorAuthenticationServiceModel : DbContext
    {
        public QuickFactorAuthenticationServiceModel()
            : base("name=QuickFactorAuthenticationServiceModel")
        {
        }

        public virtual DbSet<authentication_info> authentication_info { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<authentication_info>()
                .Property(e => e.RECORD_UID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.NUID)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.PIN)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.FIRST_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.MIDDLE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.LAST_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.Full_Name)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.QUESTION)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.REGION)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.TARGET_RESET_SYSTEM)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.ID1)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.ID2)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.ID3)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.ID5)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.ID6)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.ANSWER)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.EMAIL_ID)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.Added_By)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.Added_By_Date)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.Added_Date)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.Reg_By)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.REG_PIN)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.SS_Question1)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.SS_Answer1)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.SS_Question2)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.SS_Answer2)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.SS_Question3)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.SS_Answer3)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.HD_Question)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.HD_Answer)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.IVR_Question)
                .IsUnicode(false);

            modelBuilder.Entity<authentication_info>()
                .Property(e => e.IVR_Answer)
                .IsUnicode(false);
        }
    }
}
