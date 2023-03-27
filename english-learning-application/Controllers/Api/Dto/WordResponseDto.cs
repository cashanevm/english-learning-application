using System;
using english_learning_application.Models;
using System.Xml.Linq;

namespace english_learning_application.Controllers.Api.Dto
{
	public class WordResponseDto
	{

        public WordResponseDto(Word context)
        {
            ID = context.ID;
            TagIds = context.Tags.Select(x => x.ID).ToList();
            DisplayWordIds = context.DisplayWords.Select(x => x.ID).ToList();
            SentenceIds = context.Sentences.Select(x => x.ID).ToList();
            TranslatedSentenceIds = context.TranslatedSentences.Select(x => x.ID).ToList();
            TranslatedWordIds = context.TranslatedWords.Select(x => x.ID).ToList();
            TestIds = context.Tests.Select(x => x.ID).ToList();
            OriginalWord = context.OriginalWord;

        }

        public int ID { get; set; }

        //many Word to many Tag
        public List<int> TagIds { get; set; }

        //one Word to many DisplayWord
        public List<int> DisplayWordIds { get; set; }

        //one Word to many Sentence
        public List<int> SentenceIds { get; set; }

        // one Word to many TranslatedSentence
        public List<int> TranslatedSentenceIds { get; set; }

        // one Word to many TranslatedWord
        public List<int> TranslatedWordIds { get; set; }

        //many Word to many Test
        public List<int> TestIds { get; set; }

        //unic key
        public string OriginalWord { get; set; }
    }
}

