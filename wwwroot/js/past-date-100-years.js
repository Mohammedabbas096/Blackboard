// Client‑side custom validation for PastDate100YearsAttribute
// Mirrors .NET DateTime.Today.AddYears(-100) behavior, including the
// Feb‑29 → Feb‑28 adjustment when the target year is not a leap year.
// Works on HTML5 <input type="date"> values (YYYY‑MM‑DD).

/**
 * Pure validation helper (no jQuery) used by both the browser validator
 * and Node.js tests. Returns true if `dobStr` is a valid DOB according to
 * the reference date and maxYears constraints.
 * @param {string} dobStr - DOB in "YYYY-MM-DD" format.
 * @param {Date} referenceDate - The date that represents "today" for the check.
 * @param {number} maxYears - Maximum number of years in the past (default 100).
 */
function validateDob(dobStr, referenceDate, maxYears) {
    if (!dobStr || typeof dobStr !== "string") return false;

    // Parse the DOB parts explicitly – avoid UTC conversion.
    var parts = dobStr.split("-");
    if (parts.length !== 3) return false;
    var year = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10) - 1; // 0‑based month
    var day = parseInt(parts[2], 10);
    var dob = new Date(year, month, day);
    // Reject impossible dates (e.g., 2021‑02‑30) that would roll over.
    if (dob.getFullYear() !== year || dob.getMonth() !== month || dob.getDate() !== day) {
        return false;
    }
    dob.setHours(0, 0, 0, 0);

    var max = Number(maxYears) || 100;
    var today = new Date(referenceDate.getFullYear(), referenceDate.getMonth(), referenceDate.getDate());
    today.setHours(0, 0, 0, 0);

    var minYear = today.getFullYear() - max;
    var minDate = new Date(minYear, today.getMonth(), today.getDate());
    if (minDate.getMonth() !== today.getMonth()) {
        // month+1, day 0 gives last day of intended month (Feb‑28 when overflow occurs)
        minDate = new Date(minYear, today.getMonth() + 1, 0);
    }
    minDate.setHours(0, 0, 0, 0);

    return dob >= minDate && dob <= today;
}

if (typeof jQuery !== 'undefined' && jQuery.validator && jQuery.validator.addMethod) {
    jQuery.validator.addMethod("pastdate100years", function (value, element, param) {
        // In the browser the reference date is "today".
        var today = new Date();
        today.setHours(0, 0, 0, 0);
        return validateDob(value, today, param);
    }, "Date of birth must be within the past 100 years.");
    jQuery.validator.unobtrusive.adapters.addSingleVal("pastdate100years", "maxyears");
}

// Export helpers for Node.js regression tests (does not affect browser runtime)
if (typeof module !== "undefined" && module.exports) {
    module.exports = {
        // Calculates the minimum allowed date for a given reference date.
        calculateMinDate: function (referenceDate, maxYears) {
            var today = new Date(referenceDate.getFullYear(), referenceDate.getMonth(), referenceDate.getDate());
            today.setHours(0, 0, 0, 0);
            var minYear = today.getFullYear() - maxYears;
            var minDate = new Date(minYear, today.getMonth(), today.getDate());
            if (minDate.getMonth() !== today.getMonth()) {
                minDate = new Date(minYear, today.getMonth() + 1, 0);
            }
            minDate.setHours(0, 0, 0, 0);
            return minDate;
        },
        // Re‑export the pure validator for tests.
        validateDob: validateDob
    };
}
