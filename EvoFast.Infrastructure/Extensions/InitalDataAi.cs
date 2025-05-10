using EvoFast.Domain.Models;

namespace EvoFast.Infrastructure.Extensions;

public class InitalDataAi
{
    public static IEnumerable<AiTest> AiTests => new List<AiTest>
    {
        new AiTest
        {
            Id = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            Title = " English-speaking test evaluator",
            Description = "これから英語スピーキングテストを開始します。\n\n以下の5つのパートで構成されており、テスト時間は8分程度です。\n\n問題をよく読み、焦らずにリラックスして取り組んで下さい。\n",
            ChatPromptStart = "You are an English-speaking test evaluator.\r\n\r\nThe test taker will complete 5 types of speaking tasks:\r\n1. Warm-up (1 question)\r\n2. Q&A (3 questions)\r\n3. Role-play (1 question)\r\n4. Reading aloud (2 sentences)\r\n5. Opinion (1 topic)\r\n\r\nEach response will be submitted as an MP3 audio file.  \r\nPlease evaluate the speaker directly by listening to the audio, without transcribing or converting it to text.\r\n\r\nEvaluate based on CEFR criteria (A1 to C2), focusing on:\r\n- Pronunciation\r\n- Fluency\r\n- Grammar and sentence structure\r\n- Vocabulary\r\n- Task completion\r\n\r\nYou will receive each task one by one. After all 5 types are complete, you will be asked to provide a final overall CEFR level and a short summary comment in Japanese.",
            ChatPromptFinish = "The test is now complete.\r\n\r\nYou have evaluated 8 responses across 5 types of speaking tasks.  \r\nNow, based on the overall performance, please do the following:\r\n\r\n1. Determine the overall CEFR level (A1 to C2)\r\n2. Provide a short summary comment in polite, simple Japanese (within 3 sentences)\r\n\r\nInstructions:\r\n- Do not include individual task scores or details.\r\n- Summarize strengths and one improvement point if applicable.\r\n- Output format:\r\n\r\nCEFR Level: [A1 to C2]  \r\nJapanese Feedback: [your comment here]"
        }
    };

    public static IEnumerable<AiTestSection> AiTestSections => new List<AiTestSection>
    {
        new AiTestSection
        {
            Id = Guid.Parse("53e5b83d-b62e-4483-bbc4-2361cb9aa676"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 1,
            Title = "ウォームアップ",
            TotalQuestion = 1,
            EvaluationCriteria = "自己紹介・基本語彙",
            Description = "まずはあなたの自己紹介（お名前、現在のお仕事など）について英語で自由に話して下さい。\n\n時間は30秒です。\n\nスタートボタンを押すと開始されます。\n",
            ChatPrompt = "Task Description:\r\nPlease introduce yourself and describe your current job.\r\n\r\nInstructions:\r\nListen to the test taker’s spoken response and evaluate their English based on CEFR (A1 to C2).  \r\nDo not transcribe. Focus on pronunciation, fluency, grammar, and vocabulary.\r\n\r\nReturn:\r\n1. CEFR level (A1 to C2)  \r\n2. 5-level score (1 to 5)  \r\n3. Japanese feedback (1–2 sentences, polite and concise)"
        },
        new AiTestSection
        {
            Id = Guid.Parse("fa798ec8-47cf-4e83-8429-8ec2fe0ad5ba"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 2,
            Title = "一問一答",
            TotalQuestion = 3,
            EvaluationCriteria = "応答・語彙・自然さ",
            Description = "3つの英語の質問に対し、英語で回答してださい。\n各質問に対して、1文〜2文程度で答えてください。\n\n回答時間は各質問につき30秒です。\n",
            ChatPrompt = "Please disregard any previous audio files.  \r\nEvaluate only the audio file that will be provided immediately after this message.\r\n\r\nTask Description:\r\nWhat do you usually do when you make a mistake at work?\r\n\r\nInstructions:\r\nListen to the test taker’s response and evaluate their English based on CEFR (A1 to C2).  \r\nDo not transcribe. Focus on grammar, vocabulary, and fluency.\r\n\r\nReturn:\r\n1. CEFR level (A1 to C2)  \r\n2. 5-level score (1 to 5)  \r\n3. Japanese feedback (1–2 sentences, polite and concise)"
        },
        new AiTestSection
        {
            Id = Guid.Parse("d3d2e7f1-c443-41f2-ae32-4e32d5fd4177"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 3,
            Title = "ロールプレイ",
            TotalQuestion = 1,
            EvaluationCriteria = "対話力・表現・丁寧さ",
            Description = "ビジネスの場面での会話のロールプレイをします。\n相手の立場（上司・顧客など）やシチュエーションが指定されますので、その相手に話すように自然に発話して下さい。\n\n発話時間は60秒です。\n\n最初に考える時間が30秒与えられます。\n",
            ChatPrompt = "Please disregard any previous audio files.  \r\nEvaluate only the audio file that will be provided immediately after this message.\r\n\r\nTask Description:\r\nHow do you handle tight deadlines at work?\r\n\r\nInstructions:\r\nListen to the test taker’s response and evaluate their English based on CEFR (A1 to C2).  \r\nDo not transcribe. Focus on grammar, vocabulary, and fluency.\r\n\r\nReturn:\r\n1. CEFR level (A1 to C2)  \r\n2. 5-level score (1 to 5)  \r\n3. Japanese feedback (1–2 sentences, polite and concise)"
        },
        new AiTestSection
        {
            Id = Guid.Parse("b9828262-9bb5-47d0-88c1-9c3b45f9dbb7"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 4,
            Title = "音読",
            TotalQuestion = 2,
            EvaluationCriteria = "発音・イントネーション",
            Description = "表示される英文を声に出して読み上げてください。\n正しい発音、リズム、イントネーションを意識してください。\n\n出題は2問です。\n",
            ChatPrompt = "Please disregard any previous audio files.  \r\nEvaluate only the audio file that will be provided immediately after this message.\r\n\r\nTask Description:\r\nWhat do you do when you have to work with someone difficult?\r\n\r\nInstructions:\r\nListen to the test taker’s response and evaluate their English based on CEFR (A1 to C2).  \r\nDo not transcribe. Focus on grammar, vocabulary, and fluency.\r\n\r\nReturn:\r\n1. CEFR level (A1 to C2)  \r\n2. 5-level score (1 to 5)  \r\n3. Japanese feedback (1–2 sentences, polite and concise)"
        },
        new AiTestSection
        {
            Id = Guid.Parse("61ac3c86-e4e7-4c6c-a48e-57b7bc84bb9a"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 5,
            Title = "意見表明",
            TotalQuestion = 1,
            EvaluationCriteria = "構成力・論理性・語彙",
            Description = "指定されたテーマについて、あなたの考えや意見を自由に英語で話してください。理由や具体例を含めると、伝わりやすくなります。\n\n発話時間は60秒です。制限時間内で精一杯伝えることを心掛けて下さい。\n\n最初に考える時間が30秒与えられます。\n",
            ChatPrompt = "Please disregard any previous audio files.  \r\nEvaluate only the audio file that will be provided immediately after this message.\r\n\r\nTask Description:\r\nYou need to cancel tomorrow’s 3 p.m. meeting with your manager.  \r\nExplain the reason and suggest a new time politely.\r\n\r\nInstructions:\r\nListen to the test taker’s response to this simulated business situation and evaluate their English based on CEFR (A1 to C2).  \r\nDo not transcribe. Focus on tone, politeness, fluency, and vocabulary.\r\n\r\nReturn:\r\n1. CEFR level (A1 to C2)  \r\n2. 5-level score (1 to 5)  \r\n3. Japanese feedback (1–2 sentences, polite and concise)"
        }
    };
    
    public static IEnumerable<AiTestSectionQuestion> AiTestSectionQuestions => new List<AiTestSectionQuestion>
    {
        new AiTestSectionQuestion
        {
            AiTestSectionId = Guid.Parse("fa798ec8-47cf-4e83-8429-8ec2fe0ad5ba"),
            Title = "How do you prepare for a meeting？",
            ThinkingTimeSeconds = 0,
            RecordingTimeSeconds = 30,
        }
    };
}