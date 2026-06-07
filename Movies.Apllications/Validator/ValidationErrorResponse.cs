using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Validator;

  public record ValidationError(string PropertyName, string Message);

  public record ValidationErrorResponse(List<ValidationError> Errors);

//اگر کلاس فقط برای جابه‌جایی اطلاعات است (مثل همین ValidationError)، record بهترین انتخاب است.
//چرا در ValidationErrorResponse از List<ValidationError> استفاده کردیم؟

// ممکن است کاربر در یک فرم، چندین فیلد را اشتباه پر کرده باشد(مثلاً هم نام خالی باشد، هم ایمیل غلط باشد). با این ساختارِ List یا “لیست”، ما به جای یک خطا، تمام خطاهای کاربر را یک‌جا بسته‌بندی می‌کنیم و می‌فرستیم.