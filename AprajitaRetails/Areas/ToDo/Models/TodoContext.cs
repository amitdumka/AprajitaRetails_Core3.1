﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.ToDo.Models
{

    public class TodoItem
    {
        [Required, Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        [MinLength(15)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public bool Done { get; set; }


        [DataType(DataType.DateTime)]
        [Column("Added")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime Added { get; set; }
        //[NotMapped]
        //public Instant Added { get; set; }


        [DataType(DataType.DateTime)]
        [Column("DueTo")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime DueTo { get; set; }
        // [NotMapped]
        //public Instant DueTo { get; set; }

        public FileInfo File { get; set; }

        //[Obsolete ("Property only used for EF-serialization purposes")]
        //[DataType (DataType.DateTime)]
        //[Column ("Added")]
        //[EditorBrowsable (EditorBrowsableState.Never)]
        //public DateTime AddedDateTime
        //{
        //    get=>  Added.ToDateTimeUtc ();
        //    set =>  Added= DateTime.SpecifyKind (value, DateTimeKind.Utc).ToInstant ();
        //}

        //[Obsolete ("Property only used for EF-serialization purposes")]
        //[DataType (DataType.DateTime)]
        //[Column ("DueTo")]
        //[EditorBrowsable (EditorBrowsableState.Never)]
        //public DateTime DuetoDateTime
        //{
        //    get => DueTo.ToDateTimeUtc ();
        //    set =>DueTo= DateTime.SpecifyKind (value, DateTimeKind.Utc).ToInstant ();
        //}

        [Column("Tags")]
        [MaxLength(Constants.MAX_TAGS)]
        public IEnumerable<string> Tags { get; set; }
        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        //TODO: to make TODO list for particular user.
        //[Required]
        //[MaxLength(450)]
        //public string AssignedUserId { get; set; }
        //[Display(Name = "Private")]
        //public bool IsPrivate { get; set; }
    }

    public class FileInfo
    {
        [Required, Key]
        public Guid TodoId { get; set; }

        [MaxLength(500)]
        public string Path { get; set; }

        public long Size { get; set; }
    }
}
