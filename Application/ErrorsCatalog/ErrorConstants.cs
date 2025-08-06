namespace Application.ErrorsCatalog
{
    public static class ErrorConstants
    {
        // Not documented error
        public static string Generic00000 = "G00000";

        // CreateCourse
        public static CodePropertyNamePair CreateCourseErrorFormat00001 = new("F00001", "name");
        public static CodePropertyNamePair CreateCourseErrorFormat00002 = new("F00002", "name");
        public static CodePropertyNamePair CreateCourseErrorFormat00003 = new("F00003", "teacher_id");
        public static CodePropertyNamePair CreateCourseErrorFormat00004 = new("F00004", "teacher_id");
        public static CodePropertyNamePair CreateCourseErrorFormat00005 = new("F00005", "code");
        public static CodePropertyNamePair CreateCourseErrorFormat00006 = new("F00006", "code");
        public static CodePropertyNamePair CreateCourseErrorFormat00007 = new("F00007", "description");
        public static CodePropertyNamePair CreateCourseErrorFormat00008 = new("F00008", "description");

        public static CodePropertyNamePair CreateCourseErrorConflict00001 = new("C00001", "code");
        public static CodePropertyNamePair CreateCourseErrorConflict00002 = new("C00002", "teacher_id");
    }
}
