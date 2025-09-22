namespace Parentee_BE.API.Constants;

public class APIEndpointsConstant
{
    public const string ROOT_ENDPOINT = "/api";
    public const string API_VERSION = "/v1";
    public const string API_ENDPOINT = ROOT_ENDPOINT + API_VERSION;
    
    public static class AiEndpoints
    {
        public const string CHAT_ENDPOINT = API_ENDPOINT + "/chat";
    }
    
    public static class AuthEndpoints
    {
        public const string AUTH_ENDPOINT = API_ENDPOINT + "/auth";
        public const string LOGIN_ENDPOINT = AUTH_ENDPOINT + "/login";
        public const string SIGNIN_GOOGLE = AUTH_ENDPOINT + "/signin-google";
        public const string GOOGLE_RESPONSE = AUTH_ENDPOINT + "/google-response";
    }
    
    public static class AccountEndpoints
    {
        public const string ACCOUNT_ENDPOINT = API_ENDPOINT + "/account";
        public const string GET_ACCOUNT_ENDPOINT = ACCOUNT_ENDPOINT;
        public const string GET_CURRENT_ACCOUNT_ENDPOINT = ACCOUNT_ENDPOINT + "/current";
        public const string GET_MANY_ACCOUNTS_ENDPOINT = ACCOUNT_ENDPOINT + "/many";
        public const string GET_ACCOUNT_BY_ID_ENDPOINT = ACCOUNT_ENDPOINT + "/{id}";
        public const string CREATE_ACCOUNT_ENDPOINT = ACCOUNT_ENDPOINT;
        public const string UPDATE_ACCOUNT_ENDPOINT = ACCOUNT_ENDPOINT + "/{id}";
        public const string DELETE_ACCOUNT_ENDPOINT = ACCOUNT_ENDPOINT + "/{id}";
    }

    public static class ChildEndpoints
    {
        public const string CREATE_CHILD_ENDPOINT = API_ENDPOINT + "/create";
        public const string VIEW_CHILD_ENDPOINT = API_ENDPOINT + "/view";
        public const string GET_CHILD_BY_ID_ENDPOINT = API_ENDPOINT + "/{id}";
        public const string UPDATE_CHILD_ENDPOINT = API_ENDPOINT + "/{id}";
        public const string DELETE_CHILD_ENDPOINT = API_ENDPOINT + "/{id}";
    }
    
    public static class FeedingEndpoints
    {
        public const string FEEDING_ENDPOINT = API_ENDPOINT + "/feeding";
        public const string CREATE_FEEDING_ENDPOINT = API_ENDPOINT;
        public const string GET_FEEDING_BY_ID_ENDPOINT = API_ENDPOINT + "/{id}";
        public const string UPDATE_FEEDING_ENDPOINT = API_ENDPOINT + "/{id}";
        public const string DELETE_FEEDING_ENDPOINT = API_ENDPOINT + "/{id}";
    }
    
    public static class DiaperChangeEndpoints
    {
        public const string DIAPERCHANGE_ENDPOINT = API_ENDPOINT + "/diaperchange";
        public const string CREATE_DIAPERCHANGE_ENDPOINT = DIAPERCHANGE_ENDPOINT;
        public const string GET_DIAPERCHANGE_BY_ID_ENDPOINT = DIAPERCHANGE_ENDPOINT + "/{id}";
        public const string UPDATE_DIAPERCHANGE_ENDPOINT = DIAPERCHANGE_ENDPOINT + "/{id}";
        public const string DELETE_DIAPERCHANGE_ENDPOINT = DIAPERCHANGE_ENDPOINT + "/{id}";
    }
    
    public static class MeasurementEndpoints
    {
        public const string MEASUREMENT_ENDPOINT = API_ENDPOINT + "/measurement";
        public const string CREATE_MEASUREMENT_ENDPOINT = MEASUREMENT_ENDPOINT;
        public const string GET_MEASUREMENT_BY_ID_ENDPOINT = MEASUREMENT_ENDPOINT + "/{id}";
        public const string UPDATE_MEASUREMENT_ENDPOINT = MEASUREMENT_ENDPOINT + "/{id}";
        public const string DELETE_MEASUREMENT_ENDPOINT = MEASUREMENT_ENDPOINT + "/{id}";
    }
    
    public static class SleepEndpoints
    {
        public const string SLEEP_ENDPOINT = API_ENDPOINT + "/sleep";
        public const string CREATE_SLEEP_ENDPOINT = SLEEP_ENDPOINT;
        public const string GET_SLEEP_BY_ID_ENDPOINT = SLEEP_ENDPOINT + "/{id}";
        public const string UPDATE_SLEEP_ENDPOINT = SLEEP_ENDPOINT + "/{id}";
        public const string DELETE_SLEEP_ENDPOINT = SLEEP_ENDPOINT + "/{id}";
    }
    
    public static class TaskEndpoints
    {
        public const string TASK_ENDPOINT = API_ENDPOINT + "/task";
        public const string CREATE_TASK_ENDPOINT = TASK_ENDPOINT;
        public const string GET_TASK_BY_ID_ENDPOINT = TASK_ENDPOINT + "/{id}";
        public const string UPDATE_TASK_ENDPOINT = TASK_ENDPOINT + "/{id}";
        public const string DELETE_TASK_ENDPOINT = TASK_ENDPOINT + "/{id}";
    }

}