import { CalendarConfigModel } from "../models/helper/calendar/calendar-config.model";

export function getConfigCalendarBR() : CalendarConfigModel {
    return {
        closeText: "Limpar",
        prevText: "Anterior",
        nextText: "Próximo",
        currentText: "Hoje",
        monthNames: [ "janeiro", "fevereiro", "março", "abril", "maio", "junho",
        "julho", "agosto", "setembro", "outubro", "novembro", "dezembro" ],
        monthNamesShort: [ "jan.", "fev.", "març.", "abr.", "mai.", "junh.",
        "julh..", "agos.", "set.", "out.", "nov.", "dez." ],
        dayNames: [ "domingo", "segunda", "terça", "quarta", "quinta", "sexta", "sábado" ],
        dayNamesShort: [ "dom.", "seg.", "ter.", "qua.", "qui.", "sex.", "sáb." ],
        dayNamesMin: [ "D","S","T","Q","Q","S","S" ],
        weekHeader: "Semana",
        dateFormat: "dd/mm/yy",
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ""
    } as CalendarConfigModel;
}