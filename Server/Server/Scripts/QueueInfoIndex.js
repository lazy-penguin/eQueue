M.AutoInit();

$(document).ready(function () {
    $('.modal').modal();
});

createQueue = function () {
    queueName = $('#queueName')[0].value;
    queueExpires = $('#timer')[0].value;
    newQueueElement = document.createElement('a');
    newQueueElement.classList.add('collection-item');
    newQueueElement.classList.add('deep-purple');
    newQueueElement.classList.add('lighten-4');
    newQueueElement.classList.add('black-text');
    newQueueElement.text = queueName
    $('#queues')[0].append(newQueueElement);
}