﻿using App.Business.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.DTOs.SettingDTOs
{
    public class SettingDTO : BaseEntityDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
