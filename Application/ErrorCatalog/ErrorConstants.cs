namespace Application.ErrorCatalog
{
    public static class ErrorConstants
    {
        // Not documented error
        public static string Generic00000 = "G00000";

        // Paginated
        public static ErrorTuple PaginatedFormat00001 = new("Paginated-F00001", "current_page");
        public static ErrorTuple PaginatedFormat00002 = new("Paginated-F00002", "page_size");

        // BasicSearch
        public static ErrorTuple BasicSearchFormat00001 = new("BasicSearch-F00001", "text_filter");

        // Code
        public static ErrorTuple CodeFormat00001 = new("Code-F00001", "code");

        // Id
        public static ErrorTuple IdFormat00001 = new("Id-F00001", "id");
        public static ErrorTuple IdFormat00002 = new("Id-F00002", "id");

        // Ids
        public static ErrorTuple IdsFormat00001 = new("Ids-F00001", "ids");
        public static ErrorTuple IdsFormat00002 = new("Ids-F00002", "ids");
        public static ErrorTuple IdsFormat00003 = new("Ids-F00003", "ids");
        public static ErrorTuple IdsFormat00004 = new("Ids-F00004", "ids");

        // Guid
        public static ErrorTuple GuidFormat00001 = new("Guid-F00001", "id");
        public static ErrorTuple GuidFormat00002 = new("Guid-F00002", "id");

        // Guids
        public static ErrorTuple GuidsFormat00001 = new("Guids-F00001", "ids");
        public static ErrorTuple GuidsFormat00002 = new("Guids-F00002", "ids");
        public static ErrorTuple GuidsFormat00003 = new("Guids-F00003", "ids");
        public static ErrorTuple GuidsFormat00004 = new("Guids-F00004", "ids");

        // CreateCourse
        public static ErrorTuple CreateCourseFormat00001 = new("CreateCourse-F00001", "name");
        public static ErrorTuple CreateCourseFormat00002 = new("CreateCourse-F00002", "name");
        public static ErrorTuple CreateCourseFormat00003 = new("CreateCourse-F00003", "teacher_id");
        public static ErrorTuple CreateCourseFormat00004 = new("CreateCourse-F00004", "teacher_id");
        public static ErrorTuple CreateCourseFormat00005 = new("CreateCourse-F00005", "code");
        public static ErrorTuple CreateCourseFormat00006 = new("CreateCourse-F00006", "code");
        public static ErrorTuple CreateCourseFormat00007 = new("CreateCourse-F00007", "description");
        public static ErrorTuple CreateCourseFormat00008 = new("CreateCourse-F00008", "description");
        public static ErrorTuple CreateCourseContent00001 = new("CreateCourse-C00001", "code");
        public static ErrorTuple CreateCourseContent00002 = new("CreateCourse-C00002", "teacher_id");

        // UpdateCourse
        public static ErrorTuple UpdateCourseFormat00001 = new("UpdateCourse-F00001", "name");
        public static ErrorTuple UpdateCourseFormat00002 = new("UpdateCourse-F00002", "teacher_id");
        public static ErrorTuple UpdateCourseFormat00003 = new("UpdateCourse-F00003", "code");
        public static ErrorTuple UpdateCourseFormat00004 = new("UpdateCourse-F00004", "description");
        public static ErrorTuple UpdateCourseContent00001 = new("UpdateCourse-C00001", "id");
        public static ErrorTuple UpdateCourseContent00002 = new("UpdateCourse-C00002", "code");
        public static ErrorTuple UpdateCourseContent00003 = new("UpdateCourse-C00003", "teacher_id");

        // GetCourseByCode
        public static ErrorTuple GetCourseByCodeContent00001 = new("GetCourseByCode-C00001", "code");

        // GetCourseById
        public static ErrorTuple GetCourseByIdContent00001 = new("GetCourseById-C00001", "id");

        // GetStudentsByCourseId
        public static ErrorTuple GetStudentsByCourseIdFormat00001 = new("GetStudentsByCourseId-F00001", "course_id");
        public static ErrorTuple GetStudentsByCourseIdFormat00002 = new("GetStudentsByCourseId-F00002", "course_id");
        public static ErrorTuple GetStudentsByCourseIdContent00001 = new("GetStudentsByCourseId-C00001", "id");

        // SearchCoursesByObject
        public static ErrorTuple SearchCoursesByObjectFormat00001 = new("SearchCoursesByObject-F00001", "code");
        public static ErrorTuple SearchCoursesByObjectFormat00002 = new("SearchCoursesByObject-F00002", "code");
        public static ErrorTuple SearchCoursesByObjectFormat00003 = new("SearchCoursesByObject-F00003", "name");
        public static ErrorTuple SearchCoursesByObjectFormat00004 = new("SearchCoursesByObject-F00004", "name");
        public static ErrorTuple SearchCoursesByObjectFormat00005 = new("SearchCoursesByObject-F00005", "description");
        public static ErrorTuple SearchCoursesByObjectFormat00006 = new("SearchCoursesByObject-F00006", "description");
        public static ErrorTuple SearchCoursesByObjectFormat00007 = new("SearchCoursesByObject-F00007", "created_at");
        public static ErrorTuple SearchCoursesByObjectFormat00008 = new("SearchCoursesByObject-F00008", "created_at");
        public static ErrorTuple SearchCoursesByObjectFormat00009 = new("SearchCoursesByObject-F00009", "code");
        public static ErrorTuple SearchCoursesByObjectFormat00010 = new("SearchCoursesByObject-F00010", "name");
        public static ErrorTuple SearchCoursesByObjectFormat00011 = new("SearchCoursesByObject-F00011", "description");
        public static ErrorTuple SearchCoursesByObjectFormat00012 = new("SearchCoursesByObject-F00012", "created_at");

        // CreateEnrollment
        public static ErrorTuple CreateEnrollmentFormat00001 = new("CreateEnrollment-F00001", "course_id");
        public static ErrorTuple CreateEnrollmentFormat00002 = new("CreateEnrollment-F00002", "course_id");
        public static ErrorTuple CreateEnrollmentFormat00003 = new("CreateEnrollment-F00003", "student_id");
        public static ErrorTuple CreateEnrollmentFormat00004 = new("CreateEnrollment-F00004", "student_id");
        public static ErrorTuple CreateEnrollmentContent00001 = new("CreateEnrollment-C00001", "id");
        public static ErrorTuple CreateEnrollmentContent00002 = new("CreateEnrollment-C00002", "course_id");
        public static ErrorTuple CreateEnrollmentContent00003 = new("CreateEnrollment-C00003", "student_id");

        // CreateStudent
        public static ErrorTuple CreateStudentFormat00001 = new("CreateStudent-F00001", "code");
        public static ErrorTuple CreateStudentFormat00002 = new("CreateStudent-F00002", "code");
        public static ErrorTuple CreateStudentFormat00003 = new("CreateStudent-F00003", "firstname");
        public static ErrorTuple CreateStudentFormat00004 = new("CreateStudent-F00004", "firstname");
        public static ErrorTuple CreateStudentFormat00005 = new("CreateStudent-F00005", "lastname");
        public static ErrorTuple CreateStudentFormat00006 = new("CreateStudent-F00006", "lastname");
        public static ErrorTuple CreateStudentFormat00007 = new("CreateStudent-F00007", "birth_date");
        public static ErrorTuple CreateStudentFormat00008 = new("CreateStudent-F00008", "birth_date");
        public static ErrorTuple CreateStudentContent00001 = new("CreateStudent-C00001", "code");

        // UpdateStudent
        public static ErrorTuple UpdateStudentFormat00001 = new("UpdateStudent-F00001", "code");
        public static ErrorTuple UpdateStudentFormat00002 = new("UpdateStudent-F00002", "firstname");
        public static ErrorTuple UpdateStudentFormat00003 = new("UpdateStudent-F00003", "lastname");
        public static ErrorTuple UpdateStudentFormat00004 = new("UpdateStudent-F00004", "birth_date");
        public static ErrorTuple UpdateStudentContent00001 = new("UpdateStudent-C00001", "id");
        public static ErrorTuple UpdateStudentContent00002 = new("UpdateStudent-C00002", "code");

        // GetCoursesByStudentId
        public static ErrorTuple GetCoursesByStudentIdFormat00001 = new("GetCoursesByStudentId-F00001", "student_id");
        public static ErrorTuple GetCoursesByStudentIdFormat00002 = new("GetCoursesByStudentId-F00002", "student_id");
        public static ErrorTuple GetCoursesByStudentIdContent00001 = new("GetCoursesByStudentId-C00001", "student_id");

        // GetStudentByCode
        public static ErrorTuple GetStudentByCodeContent00001 = new("GetStudentByCode-C00001", "code");

        // GetStudentById
        public static ErrorTuple GetStudentByIdContent00001 = new("GetStudentById-C00001", "id");

        // SearchStudentsByObject
        public static ErrorTuple SearchStudentsByObjectFormat00001 = new("SearchStudentsByObject-F00001", "code");
        public static ErrorTuple SearchStudentsByObjectFormat00002 = new("SearchStudentsByObject-F00002", "code");
        public static ErrorTuple SearchStudentsByObjectFormat00003 = new("SearchStudentsByObject-F00003", "firstname");
        public static ErrorTuple SearchStudentsByObjectFormat00004 = new("SearchStudentsByObject-F00004", "firstname");
        public static ErrorTuple SearchStudentsByObjectFormat00005 = new("SearchStudentsByObject-F00005", "lastname");
        public static ErrorTuple SearchStudentsByObjectFormat00006 = new("SearchStudentsByObject-F00006", "lastname");
        public static ErrorTuple SearchStudentsByObjectFormat00007 = new("SearchStudentsByObject-F00007", "birth_date");
        public static ErrorTuple SearchStudentsByObjectFormat00008 = new("SearchStudentsByObject-F00008", "birth_date");
        public static ErrorTuple SearchStudentsByObjectFormat00009 = new("SearchStudentsByObject-F00009", "created_at");
        public static ErrorTuple SearchStudentsByObjectFormat00010 = new("SearchStudentsByObject-F00010", "created_at");
        public static ErrorTuple SearchStudentsByObjectFormat00011 = new("SearchStudentsByObject-F00011", "code");
        public static ErrorTuple SearchStudentsByObjectFormat00012 = new("SearchStudentsByObject-F00012", "firstname");
        public static ErrorTuple SearchStudentsByObjectFormat00013 = new("SearchStudentsByObject-F00013", "lastname");
        public static ErrorTuple SearchStudentsByObjectFormat00014 = new("SearchStudentsByObject-F00014", "birth_date");
        public static ErrorTuple SearchStudentsByObjectFormat00015 = new("SearchStudentsByObject-F00015", "created_at");

        // CreateTeacher
        public static ErrorTuple CreateTeacherFormat00001 = new("CreateTeacher-F00001", "code");
        public static ErrorTuple CreateTeacherFormat00002 = new("CreateTeacher-F00002", "code");
        public static ErrorTuple CreateTeacherFormat00003 = new("CreateTeacher-F00003", "firstname");
        public static ErrorTuple CreateTeacherFormat00004 = new("CreateTeacher-F00004", "firstname");
        public static ErrorTuple CreateTeacherFormat00005 = new("CreateTeacher-F00005", "lastname");
        public static ErrorTuple CreateTeacherFormat00006 = new("CreateTeacher-F00006", "lastname");
        public static ErrorTuple CreateTeacherContent00001 = new("CreateTeacher-C00001", "code");

        // UpdateTeacher
        public static ErrorTuple UpdateTeacherFormat00001 = new("UpdateTeacher-F00001", "code");
        public static ErrorTuple UpdateTeacherFormat00002 = new("UpdateTeacher-F00002", "firstname");
        public static ErrorTuple UpdateTeacherFormat00003 = new("UpdateTeacher-F00003", "lastname");
        public static ErrorTuple UpdateTeacherContent00001 = new("UpdateTeacher-C00001", "id");
        public static ErrorTuple UpdateTeacherContent00002 = new("UpdateTeacher-C00002", "code");

        // GetCoursesByTeacherId
        public static ErrorTuple GetCoursesByTeacherIdFormat00001 = new("GetCoursesByTeacherId-F00001", "teacher_id");
        public static ErrorTuple GetCoursesByTeacherIdFormat00002 = new("GetCoursesByTeacherId-F00002", "teacher_id");
        public static ErrorTuple GetCoursesByTeacherIdContent00001 = new("GetCoursesByTeacherId-C00001", "teacher_id");

        // GetStudentsByTeacherId
        public static ErrorTuple GetStudentsByTeacherIdFormat00001 = new("GetStudentsByTeacherId-F00001", "teacher_id");
        public static ErrorTuple GetStudentsByTeacherIdFormat00002 = new("GetStudentsByTeacherId-F00002", "teacher_id");
        public static ErrorTuple GetStudentsByTeacherIdContent00001 = new("GetStudentsByTeacherId-C00001", "teacher_id");

        // GetTeacherByCode
        public static ErrorTuple GetTeacherByCodeContent00001 = new("GetTeacherByCode-C00001", "code");

        // GetTeacherById
        public static ErrorTuple GetTeacherByIdContent00001 = new("GetTeacherById-C00001", "id");

        // SearchTeachersByObject
        public static ErrorTuple SearchTeachersByObjectFormat00001 = new("SearchTeachersByObject-F00001", "code");
        public static ErrorTuple SearchTeachersByObjectFormat00002 = new("SearchTeachersByObject-F00002", "code");
        public static ErrorTuple SearchTeachersByObjectFormat00003 = new("SearchTeachersByObject-F00003", "firstname");
        public static ErrorTuple SearchTeachersByObjectFormat00004 = new("SearchTeachersByObject-F00004", "firstname");
        public static ErrorTuple SearchTeachersByObjectFormat00005 = new("SearchTeachersByObject-F00005", "lastname");
        public static ErrorTuple SearchTeachersByObjectFormat00006 = new("SearchTeachersByObject-F00006", "lastname");
        public static ErrorTuple SearchTeachersByObjectFormat00007 = new("SearchTeachersByObject-F00007", "created_at");
        public static ErrorTuple SearchTeachersByObjectFormat00008 = new("SearchTeachersByObject-F00008", "created_at");
        public static ErrorTuple SearchTeachersByObjectFormat00009 = new("SearchTeachersByObject-F00009", "code");
        public static ErrorTuple SearchTeachersByObjectFormat00010 = new("SearchTeachersByObject-F00010", "firstname");
        public static ErrorTuple SearchTeachersByObjectFormat00011 = new("SearchTeachersByObject-F00011", "lastname");
        public static ErrorTuple SearchTeachersByObjectFormat00012 = new("SearchTeachersByObject-F00012", "created_at");
    }
}
