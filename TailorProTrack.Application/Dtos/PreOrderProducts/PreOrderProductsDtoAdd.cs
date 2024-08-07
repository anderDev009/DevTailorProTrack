﻿using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Application.Dtos.PreOrderProducts
{
    public class PreOrderProductsDtoAdd
    {
        public int FkProduct { get; set; }
        public int FkSize { get; set; }
        public int Quantity { get; set; }
        public int FkColorPrimary { get; set; }
        public int FkColorSecondary { get; set; }
        public decimal customPrice { get; set; }

        public int User {  get; set; }



        public static bool IsValidToAdd(int id, IPreOrderRepository preOrderRepository)
        {
	        if (!preOrderRepository.PreOrderIsEditable(id))
	        {
		        return false;
	        }

	        return true;
        }
    }
}
