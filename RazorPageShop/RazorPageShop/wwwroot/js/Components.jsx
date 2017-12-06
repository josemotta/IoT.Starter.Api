//IMPORTANT:
//==========
//I got support for jsx files through these updates:
//https://visualstudiogallery.msdn.microsoft.com/6edc26d4-47d8-4987-82ee-7c820d79be1d
//https://visualstudiogallery.msdn.microsoft.com/f3b504c6-0095-42f1-a989-51d5fc2a8459

var Panel = ReactBootstrap.Panel;
var Row = ReactBootstrap.Row;
var Column = ReactBootstrap.Col;
var Button = ReactBootstrap.Button;
var ButtonGroup = ReactBootstrap.ButtonGroup;

class Dollars extends React.Component {
    render() {
        var value = this.props.val;

        if (value != null) {
            var d = value.toFixed(2).replace(/./g, function (c, i, a) {
                return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c;
            });

            return (<span>${d}</span>);
        }
        else
            return (<span>NADA</span>);
    }
}

class Pluralize extends React.Component {
    render() {
        var value = this.props.value;
        var singular = this.props.singular;
        var plural = this.props.plural;

        if (value && !isNaN(value)) {
            var d = value.toFixed(2).replace(/./g, function (c, i, a) {
                return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c;
            });

            return (<span>{value <=1 ? singular : plural}</span>);
        }
        else
            return null;
    }
}

