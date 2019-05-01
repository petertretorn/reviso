import React, { Component } from 'react';

export default function formModalBase(ModalForm, submitHandler, isOpen=false) {

    return class extends Component {
        state = {
            isModalOpen: isOpen,
            formData: {},
        }
    
        toggleModal = () => {
            this.setState({
                isModalOpen: !this.state.isModalOpen,
            });
        } 
    
        onFormInputChange = e => {
            const obj = { [e.target.name]: e.target.value };
    
            this.setState(state => ({
                formData: { ...state.formData, ...obj }
            }));
        };
    
        submit = async (e) => {
            e.preventDefault();
        
            submitHandler(this.state.formData);
            this.toggleModal();
        };

        render() {
            return (
                <ModalForm 
                    toggleModal={this.toggleModal}
                    onFormInputChange={this.onFormInputChange}
                    submit={this.submit}
                    isOpen={this.state.isModalOpen}
                    {...this.props}
                />
            )
        }
    }
}