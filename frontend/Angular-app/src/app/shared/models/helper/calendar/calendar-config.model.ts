export interface CalendarConfigModel {
    closeText: string,
    prevText: string,
    nextText: string,
    currentText: string,
    monthNames: string[],
    monthNamesShort: string[],
    dayNames: string[],
    dayNamesShort: string[],
    dayNamesMin: string[],
    weekHeader: string,
    dateFormat: string,
    firstDay: number,
    isRTL: boolean,
    showMonthAfterYear: boolean,
    yearSuffix: string
};