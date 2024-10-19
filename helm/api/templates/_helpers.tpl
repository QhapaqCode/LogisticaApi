{{- define "logisticaapi.name" -}}
logisticaapi
{{- end }}

{{- define "logisticaapi.fullname" -}}
{{ .Release.Name }}-logisticaapi
{{- end }}

{{- define "logisticaapi.selectorLabels" -}}
logisticaapi
{{- end }}
