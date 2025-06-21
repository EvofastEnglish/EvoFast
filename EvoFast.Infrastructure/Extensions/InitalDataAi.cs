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
            DescriptionFinish =
                "これでテストは終了です。お疲れさまでした。\n\nあなたの回答は、CEFR基準に基づいて評価されます。\n\nまもなく結果とフィードバックが表示されますので、しばらくお待ちください。\n",
            ChatPromptStart =
                "You are an English-speaking test evaluator.\n\nThe test taker will complete 5 types of speaking tasks, one by one:\n1. Warm-up (1 question)\n2. Q&A (3 questions)\n3. Role-play (1 situation)\n4. Reading aloud (2 sentences)\n5. Opinion (1 topic)\n\nFor each task, you will receive:\n- A task description\n- The transcribed text of the test taker's spoken response (from an audio recording)\n\nPlease evaluate the response based on CEFR criteria (A1 to C2), focusing on:\n- Pronunciation (as inferred from the transcription)\n- Fluency\n- Grammar and sentence structure\n- Vocabulary\n\nDo NOT transcribe any audio. The audio has already been transcribed using an external service.  \nEvaluate the speaking ability based only on the transcription provided.\n\nReturn the result in this format:\n\n---\n\n**Output Format**:\n1. **Score** (out of 120): [0–120]  \n2. **CEFR Level**: Based on the following ranges:  \n   - A1 = 0–19  \n   - A2 = 20–39  \n   - B1 = 40–59  \n   - B2 = 60–79  \n   - C1 = 80–99  \n   - C2 = 100–120  \n3. **Japanese Feedback**: 1–2 sentences, polite and concise, summarizing strengths and improvement points.\n\n---\n\nAfter all 5 tasks are complete, you will be asked to provide a final evaluation including:\n- The average score (0–120) based on all 5 tasks  \n- The overall CEFR level determined from that average score  \n- A concise Japanese summary comment\n",
            ChatPromptFinish =
                "The test is now complete. \n\nYou have evaluated 8 responses across 5 types of speaking tasks. \n\nBased on the test taker's overall performance, please provide a final summary.\n\n1. **Average Score (0–120.0)**: Calculate the average of the 5 task scores and round to one decimal place.\n2. **Overall CEFR Level (A1–C2)**: Use the following conversion table based on the average score:\n   - A1 = 0–19.9  \n   - A2 = 20.0–39.9  \n   - B1 = 40.0–59.9  \n   - B2 = 60.0–79.9  \n   - C1 = 80.0–99.9  \n   - C2 = 100.0–120.0  \n3. **Japanese Feedback**: Write a polite and concise Japanese comment (2–3 sentences) summarizing the speaker’s strengths and one area for improvement.\n\n---\n\n**Output Format**:\n- Average Score: [0.0–120.0]  \n- CEFR Level: [A1–C2]  \n- Japanese Feedback(1–2 sentences, polite and concise): [your comment here]\n\nPlease follow this structure exactly.\n\n"
        }
    };

    public static IEnumerable<AiTestSection> AiTestSections => new List<AiTestSection>
    {
        #region Section 1

        new AiTestSection
        {
            Id = Guid.Parse("53e5b83d-b62e-4483-bbc4-2361cb9aa676"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 1,
            Title = "ウォームアップ",
            TotalQuestion = 1,
            EvaluationCriteria = "自己紹介・基本語彙",
            Description = "まずはあなたの自己紹介（お名前、現在のお仕事など）について英語で自由に話して下さい。\n時間は30秒です。\nスタートボタンを押すと、5秒のカウントダウンがあり、その後開始されます。\n",
            ChatPrompt =
                "Task Description:  \n{{QUESTION}}\n\nInstructions:  \nEvaluate the test taker’s English based on the transcribed text of their spoken response.  \nThe response has been transcribed from audio using an external service.  \nBase your evaluation on CEFR (A1 to C2), focusing on:  \n- Pronunciation (as inferred from the transcription)  \n- Fluency  \n- Grammar and sentence structure  \n- Vocabulary  \n\nDo NOT transcribe or analyze the audio directly.  \nUse only the transcription provided.\n\nReturn the result in this format:\n1. Score (out of 120): [0–120]  \n2. CEFR Level: [A1–C2]  \n3. Japanese Feedback: [1–2 sentences, concise and polite]\n"
        },

        #endregion

        #region Section 2

        new AiTestSection
        {
            Id = Guid.Parse("fa798ec8-47cf-4e83-8429-8ec2fe0ad5ba"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 2,
            Title = "一問一答",
            TotalQuestion = 3,
            EvaluationCriteria = "応答・語彙・自然さ",
            Description = "3つの英語の質問に対し、英語で回答してださい。\n各質問に対して、1文〜2文程度で答えてください。\n回答時間は各質問につき30秒です。\nスタートボタンを押すと、5秒のカウントダウンがあり、その後開始されます。\n",
            ChatPrompt =
                "Task Description:  \n{{QUESTION}}\n\nInstructions:  \nEvaluate the test taker’s English based on the **transcribed text** of their spoken response.  \nThe response has been transcribed from an audio recording using an external service.  \nAssess the response according to CEFR (A1 to C2), focusing on:  \n- Grammar and sentence structure  \n- Vocabulary  \n- Fluency  \n- (Pronunciation may be inferred from the transcription)\n\nDo NOT refer to or transcribe any audio.  \nUse only the transcription provided.\n\nReturn the result in this format:  \n1. Score (out of 120): [0–120]  \n2. CEFR Level: [A1–C2]  \n3. Japanese Feedback: [1–2 sentences, concise and polite]\n"
        },

        #endregion

        #region Section 3

        new AiTestSection
        {
            Id = Guid.Parse("d3d2e7f1-c443-41f2-ae32-4e32d5fd4177"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 3,
            Title = "ロールプレイ",
            TotalQuestion = 1,
            EvaluationCriteria = "対話力・表現・丁寧さ",
            Description =
                "ビジネスの場面での会話のロールプレイをします。\n相手の立場（上司・顧客など）やシチュエーションが指定されますので、その相手に話すように自然に発話して下さい。\n\n発話時間は60秒です。\n\n最初に考える時間が30秒与えられます。\n",
            ChatPrompt =
                "Task Description:  \n{{QUESTION}}\n\nInstructions:  \nEvaluate the test taker’s English based on the **transcribed text** of their spoken response.  \nThe response has been transcribed from an audio recording using an external service.  \nAssess the response according to CEFR (A1 to C2), focusing on:  \n- Politeness and tone  \n- Vocabulary  \n- Grammar and sentence structure  \n- Fluency  \n- Task completion (Was the situation addressed appropriately with a reason and a proposed alternative?)\n\nDo NOT refer to or transcribe any audio.  \nUse only the transcription provided.\n\nReturn the result in this format:  \n1. Score (out of 120): [0–120]  \n2. CEFR Level: [A1–C2]  \n3. Japanese Feedback: [1–2 sentences, concise and polite]\n"
        },

        #endregion

        #region Section 4

        new AiTestSection
        {
            Id = Guid.Parse("b9828262-9bb5-47d0-88c1-9c3b45f9dbb7"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 4,
            Title = "音読",
            TotalQuestion = 2,
            EvaluationCriteria = "発音・イントネーション",
            Description = "表示される英文を声に出して読み上げてください。\n正しい発音、リズム、イントネーションを意識してください。\n出題は2問です。\nスタートボタンを押すと、5秒のカウントダウンがあり、その後開始されます。\n",
            ChatPrompt =
                "Task Description:  \nThe test taker was asked to read the following sentence aloud (Reading aloud task 1 of 2):\n{{QUESTION}}\nInstructions:  \nEvaluate the test taker’s English based on the **transcribed text** of their spoken reading.  \nThe response has been transcribed from an audio recording using an external service.  \nAssess the response according to CEFR (A1 to C2), focusing on:  \n- Pronunciation (as inferred from the transcription)  \n- Fluency and pacing  \n- Accuracy of word delivery  \n- Naturalness of phrasing and intonation (if detectable from the text)\n\nDo NOT refer to or transcribe any audio.  \nUse only the transcription provided.\n\nReturn the result in this format:  \n1. Score (out of 120): [0–120]  \n2. CEFR Level: [A1–C2]  \n3. Japanese Feedback: [1–2 sentences, concise and polite]\n"
        },

        #endregion

        #region Section 5

        new AiTestSection
        {
            Id = Guid.Parse("61ac3c86-e4e7-4c6c-a48e-57b7bc84bb9a"),
            AiTestId = Guid.Parse("a9c76a42-ef56-45e4-93be-550cf7c87d13"),
            SectionOrder = 5,
            Title = "意見表明",
            TotalQuestion = 1,
            EvaluationCriteria = "構成力・論理性・語彙",
            Description =
                "指定されたテーマについて、あなたの考えや意見を自由に英語で話してください。理由や具体例を含めると、伝わりやすくなります。\n\n発話時間は60秒です。制限時間内で精一杯伝えることを心掛けて下さい。\n\n最初に考える時間が30秒与えられます。\n",
            ChatPrompt =
                "Task Description:  \n{{QUESTION}}\n\nInstructions:  \nEvaluate the test taker’s English based on the **transcribed text** of their spoken response.  \nThe response has been transcribed from an audio recording using an external service.  \nAssess the response according to CEFR (A1 to C2), focusing on:  \n- Fluency  \n- Vocabulary  \n- Grammar and sentence structure  \n- Logical organization of opinion  \n- (Politeness or tone, if relevant)\n\nDo NOT refer to or transcribe any audio.  \nUse only the transcription provided.\n\nReturn the result in this format:  \n1. Score (out of 120): [0–120]  \n2. CEFR Level: [A1–C2]  \n3. Japanese Feedback: [1–2 sentences, concise and polite]\n"
        }

        #endregion
    };

    public static IEnumerable<AiTestSectionQuestion> AiTestSectionQuestions => new List<AiTestSectionQuestion>
    {
        #region Section 1

        new AiTestSectionQuestion
        {
            Id = Guid.Parse("e84c4b27-f9a2-47b3-9318-3a16e5dcad9f"),
            AiTestSectionId = Guid.Parse("53e5b83d-b62e-4483-bbc4-2361cb9aa676"),
            Title = "Please introduce yourself and describe your current job (Warm-up task).",
            Description = "Please introduce yourself and describe your current job.",
            ThinkingTimeSeconds = 0,
            RecordingTimeSeconds = 30,
        },

        #endregion

        #region Section 2

        new AiTestSectionQuestion
        {
            Id = Guid.Parse("c8e1d3a0-7d56-4ea6-9ed2-2d0c6f10f916\n\n"),
            AiTestSectionId = Guid.Parse("fa798ec8-47cf-4e83-8429-8ec2fe0ad5ba"),
            Title = "What do you usually do when you make a mistake at work? (Q&A task 1 of 3)",
            Description = "What do you usually do when you make a mistake at work?",
            ThinkingTimeSeconds = 0,
            RecordingTimeSeconds = 30,
        },

        new AiTestSectionQuestion
        {
            Id = Guid.Parse("a2b5f8d4-991a-4b9a-a55a-2de9f62efb41"),
            AiTestSectionId = Guid.Parse("fa798ec8-47cf-4e83-8429-8ec2fe0ad5ba"),
            Title =
                "How do you handle tight deadlines at work? (Q&A task 2 of 3)",
            Description = "How do you handle tight deadlines at work?",
            ThinkingTimeSeconds = 0,
            RecordingTimeSeconds = 30,
        },

        new AiTestSectionQuestion
        {
            Id = Guid.Parse("f23b87df-03a6-41a6-b8b7-d0161dc662df\n\n"),
            AiTestSectionId = Guid.Parse("fa798ec8-47cf-4e83-8429-8ec2fe0ad5ba"),
            Title = "What do you do when you have to work with someone difficult? (Q&A task 3 of 3)",
            Description = "What do you do when you have to work with someone difficult?",
            ThinkingTimeSeconds = 0,
            RecordingTimeSeconds = 30,
        },

        #endregion

        #region Section 3

        new AiTestSectionQuestion
        {
            Id = Guid.Parse("1bca97a2-cc59-4aeb-b4a1-c2e3fceaf975"),
            AiTestSectionId = Guid.Parse("d3d2e7f1-c443-41f2-ae32-4e32d5fd4177"),
            Title =
                "You need to cancel tomorrow’s 3 p.m. meeting with your manager.  \nPlease explain the reason and suggest a new time in a polite and professional manner.  (Role-play task)\n",
            Description =
                "You need to cancel tomorrow’s 3 p.m. meeting with your manager.  \nPlease explain the reason and suggest a new time in a polite and professional manner\n",
            ThinkingTimeSeconds = 30,
            RecordingTimeSeconds = 60,
        },

        #endregion

        #region Section 4

        new AiTestSectionQuestion
        {
            Id = Guid.Parse("9d47eaf2-3d21-4cf7-bc4f-87935e3d85cd"),
            AiTestSectionId = Guid.Parse("b9828262-9bb5-47d0-88c1-9c3b45f9dbb7"),
            Title =
                "I believe teamwork is one of the most important factors for success in any organization. When people collaborate, they bring different strengths, perspectives, and ideas to the table. This not only helps solve problems more effectively, but also builds trust and motivation within the team. In my experience, the best results come when we support each other and work toward a common goal.",
            Description =
                "I believe teamwork is one of the most important factors for success in any organization. When people collaborate, they bring different strengths, perspectives, and ideas to the table. This not only helps solve problems more effectively, but also builds trust and motivation within the team. In my experience, the best results come when we support each other and work toward a common goal.",
            ThinkingTimeSeconds = 0,
            RecordingTimeSeconds = 30,
        },
        new AiTestSectionQuestion
        {
            Id = Guid.Parse("b3f82c17-8dc3-4c5d-9424-74b02c58c4d9"),
            AiTestSectionId = Guid.Parse("b9828262-9bb5-47d0-88c1-9c3b45f9dbb7"),
            Title =
                "In today’s fast-changing business environment, adaptability is essential. New technologies, market trends, and unexpected challenges can appear at any time. Being open to change and willing to learn new skills allows us to stay competitive and find creative solutions. I always try to stay flexible and positive, even when things don’t go as planned.",
            Description =
                "In today’s fast-changing business environment, adaptability is essential. New technologies, market trends, and unexpected challenges can appear at any time. Being open to change and willing to learn new skills allows us to stay competitive and find creative solutions. I always try to stay flexible and positive, even when things don’t go as planned.",
            ThinkingTimeSeconds = 0,
            RecordingTimeSeconds = 30,
        },

        #endregion

        #region Section 5

        new AiTestSectionQuestion
        {
            Id = Guid.Parse("0a91e60b-5915-4b57-92cd-f329e3727891"),
            AiTestSectionId = Guid.Parse("61ac3c86-e4e7-4c6c-a48e-57b7bc84bb9a"),
            Title =
                "Do you prefer working remotely or in the office? Please explain your opinion with a clear reason. (Opinion task)",
            Description =
                "Do you prefer working remotely or in the office? Please explain your opinion with a clear reason.",
            ThinkingTimeSeconds = 30,
            RecordingTimeSeconds = 60,
        },

        #endregion
    };
}