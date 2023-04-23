﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using static Helpers.Constants.Enums;

namespace Helpers.Models.DtoModels.VoteDbDto
{
    [Serializable]
    public partial class VoteDto
    {
        public int VoteID { get; set; }
        public int VotingID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public StakeType Direction { get; set; }
        public string DeployHash { get; set; }
        public string WalletAddress { get; set; }
        public int? BlockchainVoteID { get; set; }

    }
}
