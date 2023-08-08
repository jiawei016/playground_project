
export class ValidateString {
    constructor(){

    }

    IsNullOrEmpty = (_value:any) => {
        if(_value == undefined || typeof(_value) == 'undefined'){
            return true;
        }
        
        const replacedText = _value.replace(/\s+/g, '');

        if(replacedText == ''){
            return true;
        }
        else{
            return false;
        }
    }
}