using dwCheckApi.DatabaseTools;
using dwCheckApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dwCheckApi.DatabaseContexts
{
    // For a description of this file, see the "Seeding" section of:
    // https://blogs.msdn.microsoft.com/dotnet/2016/09/29/implementing-seeding-custom-conventions-and-interceptors-in-ef-core-1-0/
    public static class DatabaseContextExtentsions
    {
        public static bool AllMigrationsApplied(this DwContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeedData(this DwContext context)
        {
            if (context.AllMigrationsApplied())
            {
                var dbSeeder = new DatabaseSeeder(context);
                if (!context.Books.Any())
                {
                    dbSeeder.SeedBookEntitiesFromJson();
                }
                if (!context.Characters.Any())
                {
                    dbSeeder.SeedCharacterEntitiesFromJson();
                }
                if (!context.BookCharacters.Any())
                {
                    dbSeeder.SeedBookCharacterEntriesFromJson();
                }

                context.SaveChanges();
            }
        }

        private static List<Book> GenerateAllBookEntiies()
        {       
            return new List<Book>(){
                new Book {
                    BookName = "Witches Abroad",
                    BookOrdinal = 12,
                    BookIsbn10 = "0552134651",
                    BookIsbn13 = "9780552134651",
                    BookDescription = "It seemed an easy job ... After all, how difficult could it be to make sure that a servant girl doesn't marry a prince? But for the witches Granny Weatherwax, Nanny Ogg and Magrat Garlick, traveling to the distant city of Genua, things are never that simple ...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/a/a8/Cover_Witches_Abroad.jpg"
                }, new Book {
                    BookName = "Small Gods",
                    BookOrdinal = 13,
                    BookIsbn10 = "0575052228",
                    BookIsbn13 = "9780575052222",
                    BookDescription = "Brutha is the Chosen One. His god has spoken to him, admittedly while currently in the shape of a tortoise. Brutha is a simple lad. He can't read. He can't write. He's pretty good at growing melons. And his wants are few.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/f/f1/Cover_Small_Gods.jpg"
                }, new Book {
                    BookName = "Lords and Ladies",
                    BookOrdinal = 14,
                    BookIsbn10 = "0575052236",
                    BookIsbn13 = "9780575052239",
                    BookDescription = "It's a hot Midsummer Night. The crop circles are turning up everywhere – even on the mustard-and-cress of Pewsey Ogg, aged four. And Magrat Garlick, witch, is going to be married in the morning... Everything ought to be going like a dream. But the Lancre All-Comers Morris Team have got drunk on a fairy mound and the elves have come back, bringing all those things traditionally associated with the magical, glittering realm of Faerie: cruelty, kidnapping, malice and evil, evil murder. Granny Weatherwax and her tiny argumentative coven have really got their work cut out this time... With full supporting cast of dwarfs, wizards, trolls, Morris Dancers and one orang-utan. And lots of hey-nonny-nonny and blood all over the place.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/7/7b/Cover_Lords_and_Ladies.jpg"
                }, new Book {
                    BookName = "Men at Arms",
                    BookOrdinal = 15,
                    BookIsbn10 = "0552140287",
                    BookIsbn13 = "9780552140287",
                    BookDescription = "'Be a MAN in the City Watch! The City watch needs MEN!' But what it's got includes Corporal Carrot (technically a dwarf), Lance-constable Cuddy (really a dwarf), Lance-constable Detritus (a troll), Lance-constable Angua (a woman ... most of the time) and Corporal Nobbs (disqualified from the human race for shoving). And they need all the help they can get. Because there's evil in the air and murder afoot and something very nasty in the streets. It'd help if it could all be sorted out by noon, because that's when Captain Vimes is officially retiring, handing in his badge and getting married. And since this is Ankh-Morpork, noon promises to be not just high, but stinking.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/3/37/Cover_Men_At_Arms.jpg"
                }, new Book {
                    BookName = "Soul Music",
                    BookOrdinal = 16,
                    BookIsbn10 = "0552140295",
                    BookIsbn13 = "9780552140294",
                    BookDescription = "Other children got given xylophones. Susan just had to ask her grandfather to take his vest off. Yes. There's a Death in the family. It's hard to grow up normally when Grandfather rides a white horse and wields a scythe – especially when you have to take over the family business, and everyone mistakes you for the Tooth Fairy. And especially when you have to face the new and addictive music that has entered the Discworld. It's Lawless. It changes people. It's called Music With Rocks In. It's got a beat and you can dance to it, but ... It's alive. And it won't fade away.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/d/d9/Cover_Soul_Music.jpg"
                }, new Book {
                    BookName = "Interesting Times",
                    BookOrdinal = 17,
                    BookIsbn10 = "0552142352",
                    BookIsbn13 = "9780552142359",
                    BookDescription = "Mighty Battles! Revolution! Death! War! (and his sons Terror and Panic, and daughter Clancy). The oldest and most inscrutable empire on the Discworld is in turmoil, brought about by the revolutionary treatise What I Did On My Holidays. Workers are uniting, with nothing to lose but their water buffaloes. Warlords are struggling for power. War (and Clancy) are spreading through the ancient cities. And all that stands in the way of terrible doom for everyone is: Rincewind the Wizzard, who can't even spell the word 'wizard' ... Cohen the barbarian hero, five foot tall in his surgical sandals, who has had a lifetime's experience of not dying ...and a very special butterfly.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/9/96/Cover_Interesting_Times.jpg"
                }, new Book {
                    BookName = "Maskerade",
                    BookOrdinal = 18,
                    BookIsbn10 = "0575058080",
                    BookIsbn13 = "9780575058088",
                    BookDescription = "The Opera House, Ankh-Morpork ... a huge, rambling building, where masked figures and hooded shadows do wicked deeds in the wings ... where dying the death on stage is a little bit more than just a metaphor ... where innocent young sopranos are lured to their destiny by an evil mastermind in a hideously deformed evening dress ...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/8/82/Cover_Maskerade.jpg"
                }, new Book {
                    BookName = "Feet of Clay",
                    BookOrdinal = 19,
                    BookIsbn10 = "0552142379",
                    BookIsbn13 = "9780552142373",
                    BookDescription = "A Discworld Whodunnit. Who's murdering harmless old men? Who's poisoning the Patrician? As autumn fogs hold Ankh-Morpork in their grip, the City Watch have to track down a murderer who can't be seen. Maybe the golems know something - but the solemn men of clay, who work all day and night and are never any trouble to anyone, have started to commit suicide...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/d/d3/Cover_Feet_of_Clay.jpg"
                }, new Book {
                    BookName = "Hogfather",
                    BookOrdinal = 20,
                    BookIsbn10 = "0552145424",
                    BookIsbn13 = "9780552145428",
                    BookDescription = "It's the night before Hogswatch. And it's too quiet. There's snow, there're robins, there're trees covered with decorations, but there's a notable lack of the big fat man who delivers the toys ...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/2/2a/Cover_Hogfather.jpg"
                }, new Book {
                    BookName = "Jingo",
                    BookOrdinal = 21,
                    BookIsbn10 = "0575065400",
                    BookIsbn13 = "9780575065406",
                    BookDescription = "Discworld goes to war... A weathercock has risen from the sea of Discworld. Suddenly you can tell which way the wind is blowing. A new land has surfaced, and so have old feuds. And as two armies march, Commander Vimes of the Ankh-Morpork City Watch has got just a few hours to deal with a crime so big that there's no law against it. It's called 'war.'",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/a/a6/Cover_Jingo.jpg"
                }, new Book {
                    BookName = "The Last Continent",
                    BookOrdinal = 22,
                    BookIsbn10 = "0552154180",
                    BookIsbn13 = "9780552154185",
                    BookDescription = "This is the Discworld's last continent, a completely separate creation. It's hot. It's dry... very dry. There was this thing once called The Wet, which no one now believes in. Practically everything that's not poisonous is venomous. But it's the best bloody place in the world, all right? And it'll die in a few days. Except...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/d/db/Cover_The_Last_Continent.jpg"
                }, new Book {
                    BookName = "Carpe Jugulum",
                    BookOrdinal = 23,
                    BookIsbn10 = "0552146153",
                    BookIsbn13 = "9780552146159",
                    BookDescription = "Mightily Oats has not picked a good time to be a priest. He thought he'd come to the mountain kingdom of Lancre for a simple little religious ceremony. Now he's caught up in a war between vampires and witches, and he's not sure there is a right side. There're the witches – young Agnes, who is really in two minds about everything, Magrat, who is trying to combine witchcraft and nappies, Nanny Ogg, who is far too knowing... and Granny Weatherwax, who is big trouble.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/3/34/CarpeJugulum.jpg"
                }, new Book {
                    BookName = "The Fifth Elephant",
                    BookOrdinal = 24,
                    BookIsbn10 = "0552146161",
                    BookIsbn13 = "9780552146166",
                    BookDescription = "Sam Vimes is a man on the run. Yesterday he was a duke, a chief of police and the ambassador to the mysterious, fat-rich country of Überwald. Now he has nothing but his native wit and the gloomy trousers of Uncle Vanya (don't ask). It's snowing. It's freezing. And if he can't make it through the forest to civilization there's going to be a terrible war. But there are monsters on his trail. They're bright. They're fast. They're werewolves – and they're catching up. Sam Vimes is out of time, out of luck and already out of breath...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/3/34/Cover_The_Fith_Elephant.jpg"
                }, new Book {
                    BookName = "The Truth",
                    BookOrdinal = 25,
                    BookIsbn10 = "0575063483",
                    BookIsbn13 = "9780575063488",
                    BookDescription = "William de Worde is the accidental editor of the Discworld's first newspaper. Now he must cope with the traditional perils of a journalist's life -- people who want him dead, a recovering vampire with a suicidal fascination for flash photography, some more people who want him dead in a different way and, worst of all, the man who keeps begging him to publish pictures of his humorously shaped potatoes.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/d/d0/Cover_The_Truth.jpg"
                }, new Book {
                    BookName = "Thief of Time",
                    BookOrdinal = 26,
                    BookIsbn10 = "0552148407",
                    BookIsbn13 = "9780552148405",
                    BookDescription = "Time is a resource. Everyone knows it has to be managed. And on Discworld that is the job of the Monks of History, who store it and pump it from the places where it's wasted (like underwater -- how much time does a codfish need?) to places like cities, where there's never enough time.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/4/49/Cover_Thief_of_Time.jpg"
                }, new Book {
                    BookName = "The Last Hero",
                    BookOrdinal = 27,
                    BookIsbn10 = "057506885X",
                    BookIsbn13 = "9780575068858",
                    BookDescription = "With his ancient sword and his new walking stick and his old friends – and they're very old friends – Cohen the Barbarian is going on one final quest. It's been a good life. He's going to climb the highest mountain in the Discworld and meet his gods. He doesn't like the way they let men grow old and die. It's time, in fact, to give something back.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/4/4e/Cover_The_Last_Hero.jpg"
                }, new Book {
                    BookName = "The Amazing Maurice and his Educated Rodents",
                    BookOrdinal = 28,
                    BookIsbn10 = "055255202X",
                    BookIsbn13 = "9780552552028",
                    BookDescription = "Maurice, a streetwise tomcat, has the perfect money-making scam. He's found a stupid-looking kid who plays a pipe, and he has his very own plague of rats who are strangely educated, so Maurice can no longer think of them as 'lunch'. And everyone knows the stories about rats and pipers...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/8/89/The_Amazing_Maurice_and_his_Educated_Rodents.jpg"
                }, new Book {
                    BookName = "Night Watch",
                    BookOrdinal = 29,
                    BookIsbn10 = "0552148997",
                    BookIsbn13 = "9780552148993",
                    BookDescription = "This morning, Commander Vimes of the City Watch had it all. He was a Duke. He was rich. He was respected. He had a titanium cigar case. He was about to become a father. This morning he thought longingly about the good old days. Tonight, he's in them.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/4/4f/Cover_Night_Watch.jpg"
                }, new Book {
                    BookName = "The Wee Free Men",
                    BookOrdinal = 30,
                    BookIsbn10 = "0552551864",
                    BookIsbn13 = "9780552551861",
                    BookDescription = "There's trouble on the Aching farm – a monster in the river, a headless horseman in the driveway and nightmares spreading down from the hills. And now Tiffany Aching's little brother has been stolen by the Queen of the Fairies (although Tiffany doesn't think this is entirely a bad thing).",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/1/1b/The_Wee_Free_Men.jpg"
                }, new Book {
                    BookName = "Monstrous Regiment",
                    BookOrdinal = 31,
                    BookIsbn10 = "0552212806",
                    BookIsbn13 = "9780552212809",
                    BookDescription = "It was a sudden strange fancy... Polly Perks had to become a boy in a hurry. Cutting off her hair and wearing trousers was easy. Learning to fart and belch in public and walk like an ape took more time... And now she's enlisted in the army, and searching for her lost brother. But there's a war on. There's always a war on. And Polly and her fellow recruits are suddenly in the thick of it, without any training, and the enemy is hunting them.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/9/98/Cover_Monstrous_Regiment.jpg"
                }, new Book {
                    BookName = "A Hat Full Of Sky",
                    BookOrdinal = 32,
                    BookIsbn10 = "055255264X",
                    BookIsbn13 = "9780552552646",
                    BookDescription = "A real witch can ride a broomstick, cast spells and make a proper shamble out of nothing. Eleven-year-old Tiffany Aching can't. A real witch never casually steps out of her body, leaving it empty. Tiffany does. And there's something just waiting for a handy body to take over. Something ancient and horrible, which can't die.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/3/3c/Cover_A_Hat_Full_of_Sky.jpg"
                }, new Book {
                    BookName = "Going Postal",
                    BookOrdinal = 33,
                    BookIsbn10 = "0552149438",
                    BookIsbn13 = "9780552149433",
                    BookDescription = "Moist von Lipwig is a con artist and a fraud and a man faced with a life choice: be hanged, or put Ankh-Morpork's ailing postal service back on its feet. It's a tough decision. But he's got to see that the mail gets through, come rain, hail, sleet, dogs, the Post Office Workers Friendly and Benevolent Society, the evil chairman of the Grand Trunk Semaphore Company, and a midnight killer. Getting a date with Adora Belle Dearheart would be nice, too.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/5/57/Cover_Going_Postal.jpg"
                }, new Book {
                    BookName = "Thud!",
                    BookOrdinal = 34,
                    BookIsbn10 = "0552156639",
                    BookIsbn13 = "9780552156639",
                    BookDescription = "THUD! Koom Valley? That was where the trolls ambushed the dwarfs, or the dwarfs ambushed the trolls. It was far away. It was a long time ago. But if he doesn't solve the murder of just one dwarf, Commander Sam Vimes of Ankh-Morpork City Watch is going to see the battle fought again, right outside his office (and perhaps inside his own Watch House). With his beloved Watch crumbling around him and war-drums sounding, he must unravel every clue, outwit every assassin and brave any darkness to find the solution. And darkness is following him.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/6/6a/Cover_Thud.jpg"
                }, new Book {
                    BookName = "Wintersmith",
                    BookOrdinal = 35,
                    BookIsbn10 = "0552158380",
                    BookIsbn13 = "9780552158381",
                    BookDescription = "At 9, Tiffany Aching defeated the cruel Queen of Fairyland. At 11, she battled an ancient body-stealing evil. At 13, Tiffany faces a new challenge: a boy. And boys can be a bit of a problem when you're thirteen... But the Wintersmith isn't 'exactly' a boy. He is Winter itself-snow, gales, icicles-all of it. When he has a crush on Tiffany, he may make her roses out of ice, but his nature is blizzards and avalanches. And he wants Tiffany to stay in his gleaming, frozen world. Forever.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/5/55/Cover_Wintersmith.JPG"
                }, new Book {
                    BookName = "Making Money",
                    BookOrdinal = 36,
                    BookIsbn10 = "0385613512",
                    BookIsbn13 = "9780385613514",
                    BookDescription = "It's an offer you can't refuse. Who would not to wish to be the man in charge of Ankh-Morpork's Royal Mint and the bank next door? It's a job for life. But, as former con-man Moist von Lipwig is learning, the life is not necessarily for long. The Chief Cashier is almost certainly a vampire. There's something nameless in the cellar (and the cellar itself is pretty nameless), it turns out that the Royal Mint runs at a loss. A 300 year old wizard is after his girlfriend, he's about to be exposed as a fraud, but the Assassins' Guild might get him first. In fact lot of people want him dead. Oh... and every day he has to take the Chairman for walkies. Everywhere he looks he's making enemies. What he should be doing is... Making Money!",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/7/7a/Cover_Making_Money.jpg"
                }, new Book {
                    BookName = "Unseen Academicals",
                    BookOrdinal = 37,
                    BookIsbn10 = "0552165336",
                    BookIsbn13 = "9780552165334",
                    BookDescription = "Football has come to the ancient city of Ankh-Morpork – not the old fashioned, grubby pushing and shoving, but the new, fast football with pointy hats for goalposts and balls that go gloing when you drop them. And now, the wizards of Unseen University must win a football match, without using magic, so they're in the mood for trying everything else. The prospect of the Big Match draws in a likely lad with a wonderful talent for kicking a tin can, a maker of jolly good pies, a dim but beautiful young woman who might just turn out to be the greatest fashion model there has ever been, and the mysterious Mr Nutt. (No one knows anything much about Mr Nutt, not even Mr Nutt, which worries him, too.)",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/8/83/UnseenAcademicals.jpg"
                }, new Book {
                    BookName = "I Shall Wear Midnight",
                    BookOrdinal = 38,
                    BookIsbn10 = "0385611072",
                    BookIsbn13 = "9780385611077",
                    BookDescription = "A man with no eyes. No eyes at all. Two tunnels in his head ...It's not easy being a witch, and it's certainly not all whizzing about on broomsticks, but Tiffany Aching - teen witch - is doing her best. Until something evil wakes up, something that stirs up all the old stories about nasty old witches, so that just wearing a pointy hat suddenly seems a very bad idea. Worse still, this evil ghost from the past is hunting down one witch in particular. He's hunting for Tiffany. And he's found her….",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/d/d2/ISWM.jpg"
                }, new Book {
                    BookName = "Snuff",
                    BookOrdinal = 39,
                    BookIsbn10 = "0857520849",
                    BookIsbn13 = "9780857520845",
                    BookDescription = "According to the writer of the best selling crime novel ever to have been published in the city of Ankh-Morpork, it is a truth universally acknowledged that a policeman taking a holiday would barely have had time to open his suitcase before he finds his first corpse. And Commander Sam Vimes of the Ankh-Morpork City Watch is on holiday in the pleasant and innocent countryside, but not for him a mere body in the wardrobe, but many, many bodies and an ancient crime more terrible than murder.",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/5/58/Cover_snuff.jpg"
                }, new Book {
                    BookName = "Raising Steam",
                    BookOrdinal = 40,
                    BookIsbn10 = "0857522272",
                    BookIsbn13 = "9780857522276",
                    BookDescription = "To the consternation of the patrician, Lord Vetinari, a new invention has arrived in Ankh-Morpork - a great clanging monster of a machine that harnesses the power of all of the elements: earth, air, fire and water. This being Ankh-Morpork, it's soon drawing astonished crowds, some of whom caught the zeitgeist early and arrive armed with notepads and very sensible rainwear. Moist von Lipwig is not a man who enjoys hard work - as master of the Post Office, the Mint and the Royal Bank his input is, of course, vital... but largely dependent on words, which are fortunately not very heavy and don't always need greasing. However, he does enjoy being alive, which makes a new job offer from Vetinari hard to refuse... Steam is rising over Discworld, driven by Mister Simnel, the man wi' t'flat cap and sliding rule who has an interesting arrangement with the sine and cosine. Moist will have to grapple with gallons of grease, goblins, a fat controller with a history of throwing employees down the stairs and some very angry dwarfs if he's going to stop it all going off the rails...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/1/1b/RaisingSteam.jpg"
                }, new Book {
                    BookName = "The Shepheard's Crown",
                    BookOrdinal = 41,
                    BookIsbn10 = "0857534815",
                    BookIsbn13 = "9780857534811",
                    BookDescription = "A SHIVERING OF WORLDS.Deep in the Chalk, something is stirring. The owls and the foxes can sense it, and Tiffany Aching feels it in her boots. An old enemy is gathering strength. This is a time of endings and beginnings, old friends and new, a blurring of edges and a shifting of power. Now Tiffany stands between the light and the dark, the good and the bad. As the fairy horde prepares for invasion, Tiffany must summon all the witches to stand with her. To protect the land. Her land. There will be a reckoning ...",
                    BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/b/b6/Tsc.jpg"
                }
            };
        }
    }
}