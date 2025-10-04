// namespace Parentee_BE.AI.Services;
//
// public class RecommendService
// {
//      // Save a past activity (simplified)
//     public async Task SavePastActivityAsync(string userId, string activityText, DateTime when, int childAgeMonths, double outcomeScore)
//     {
//         var docId = Guid.NewGuid().ToString();
//         var metadata = new Dictionary<string, string>
//         {
//             ["userId"] = userId,
//             ["date"] = when.ToString("o"),
//             ["childAgeMonths"] = childAgeMonths.ToString(),
//             ["outcomeScore"] = outcomeScore.ToString()
//         };
//
//         // Save text + metadata to semantic memory (vector store) — embedding generated automatically
//         await _memory.SaveInformationAsync(collection: userId, id: docId, text: activityText, role: "activity", description: "past activity", additionalMetadata: metadata);
//     }
//
//     // Recommend today's actions
//     public async Task<string> RecommendTodayAsync(string userId, int childAgeMonths, string availableTimeDescription, string goal)
//     {
//         // 1. Build query context
//         var todayCtx = $"Today: {DateTime.UtcNow:yyyy-MM-dd}. Child age: {childAgeMonths} months. Time available: {availableTimeDescription}. Goal: {goal}.";
//
//         // 2. Retrieve top-k similar past activities from user's collection
//         var results = await _memory.SearchAsync(collection: userId, query: todayCtx, limit: 10);
//
//         // 3. Build a prompt that asks the LLM to analyze retrieved items and propose 3 recommendations
//         // Create simple template — can be a semantic function file in skills folder
//         var prompt = $@"
// You are a friendly parenting assistant. Context: {todayCtx}
// Here are similar past activities (most relevant first). For each, we have text and metadata:
// {FormatSearchResults(results)}
//
// Based on these examples, suggest up to 3 specific activities for today tailored to the child's age and time. For each suggestion include: 1) Short description, 2) Why it should work (1-2 sentences), 3) Estimated time, 4) Confidence (0-100). Also rank them by best match to historical success.
// ";
//
//         // Call the kernel's chat/completion
//         var chat = _kernel.CreateSemanticFunction(prompt, maxTokens: 500);
//         var response = await chat.InvokeAsync(new ContextVariables());
//
//         return response.Result;
//     }
//
//     private string FormatSearchResults(IEnumerable<MemoryQueryResult> results)
//     {
//         int i = 1;
//         var sb = new System.Text.StringBuilder();
//         foreach (var r in results)
//         {
//             sb.AppendLine($"#{i++}: Text: {r.Text}");
//             sb.AppendLine($"   metadata: {string.Join(", ", r.)}");
//             sb.AppendLine();
//         }
//         return sb.ToString();
//     }
// }