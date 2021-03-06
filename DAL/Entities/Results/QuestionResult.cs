﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.Results
{
    public class QuestionResult: BaseEntity
    {
        public int QuestionResultId { get; set; }
        public virtual AnswerResult AnswerResult { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int KnowledgeResultId { get; set; }
        public virtual KnowledgeResult KnowledgeResult { get; set; }
    }
}
