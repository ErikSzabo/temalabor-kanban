﻿using Kanban.Bll.Models;
using Kanban.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Bll
{
    public interface ICardService
    {
        Task<CardDto> GetCard(int cardID);
        Task<CardDto> MoveCard(int moveCardID, CardMoveDto cardMove);
        Task<CardDto> UpdateCard(int cardID, CardDto card);
        Task DeleteCard(int cardID);
    }
}
