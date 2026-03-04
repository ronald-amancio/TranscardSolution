window.startIdleTimer = (dotNetHelper, timeoutMinutes) => {
    let timeoutMilliseconds = timeoutMinutes * 60 * 1000;
    let warningMilliseconds = timeoutMilliseconds - (60 * 1000);

    function resetTimer() {
        clearTimeout(window.idleTimeout);
        clearTimeout(window.warningTimeout);

        window.warningTimeout = setTimeout(() => {
            dotNetHelper.invokeMethodAsync("TriggerIdleWarning");
        }, warningMilliseconds);

        window.idleTimeout = setTimeout(() => {
            dotNetHelper.invokeMethodAsync("TriggerIdleLogout");
        }, timeoutMilliseconds);
    }

    document.addEventListener("mousemove", resetTimer);
    document.addEventListener("keypress", resetTimer);
    document.addEventListener("scroll", resetTimer);
    document.addEventListener("click", resetTimer);

    resetTimer();
};

window.restartIdleTimer = () => {
    if (window.idleTimeout) {
        clearTimeout(window.idleTimeout);
    }
    if (window.warningTimeout) {
        clearTimeout(window.warningTimeout);
    }
};