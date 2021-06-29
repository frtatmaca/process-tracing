var custom_repon_metro = function () {
    var number_input = function () {
        $('input[type="number"], input[type="text"]').live('keydown', function (event) {

            var data_type = $(this).attr('data-type');
            if (data_type !== 'number') return

            if (!(event.keyCode == 8 || event.keyCode == 46 ||
                (event.keyCode >= 35 && event.keyCode <= 40) ||
                (event.keyCode >= 48 && event.keyCode <= 57) ||
                (event.KeyCode == 19 && event.keyCode == 106) ||
                (event.keyCode >= 96 && event.keyCode <= 105))) {

                event.preventDefault();
            }
        });

        $('input[type="number"], input[type="text"]').live('keyup', function (event) {

            var data_type = $(this).attr('data-type');
            if (data_type !== 'number') return

            var given_value = parseInt($(this).val())

            var max_value = $(this).attr('data-max');
            if (max_value.length > 0 && given_value > max_value) {
                $(this).val(max_value)
            }
        });
    };

    number_input();

    var _organization_menu_nested = function () {
        $('#menu-visibility').nestable({
            group: 0,
            maxDepth: 2
        });
        $('#modules-items').nestable({
            group: 1,
            maxDepth: 1,
        });

        $("#organization-menu .dd-item > .dd3-content input[type='checkbox']:checked").each(function () {
            $(this).parents('.dd3-content').find('.info-text span.info-hide').hide().next('span.info-shown').show();
        })


        $("#organization-menu .dd-item > .dd3-content input[type='checkbox']").change(function () {
            //get current parent item
            var current_item = $(this),
                parents_item = $(this).parents('li.dd-item'),
                checked_count = parents_item.find('input[type="checkbox"]:checked').length;



            if (current_item.is(':checked')) {
                $('>.dd3-content input:checkbox', parents_item).parent('span').addClass('checked');
                $('>.dd3-content>.info-text span.info-hide', parents_item).hide().next('span.info-shown').show();
                $('>.dd3-content input:checkbox', parents_item).not($(this)).attr('checked', 'checked');
            } else {
                $(this).parents('.dd3-content').find('.info-text span.info-shown').hide().prev('span.info-hide').show();
                var child_item = $(this).parents('li.dd-item').find('input[type="checkbox"]')

                if (current_item.hasClass('main-menu')) {
                    child_item.each(function () {
                        $(this).parent('span').removeClass('checked');
                        $(this).parents('.dd3-content').find('.info-text span.info-shown').hide().prev('span.info-hide').show();
                        $(this).parent('span').find('input:checkbox').attr('checked',null);
                    })

                }
            }






        });
    };

    var _toggle_button_label = function () {
        $('.toggle-button.show-hide').toggleButtons({
            width: 90,
            label: {
                enabled: "Show",
                disabled: "Hide"
            }
        });
    }

    var add_remove_activity_class = function () {
        $('#available-class.class-items input[type="checkbox"]').on('change', function () {
            var parent = $(this).parents('.class-items'),
                selected_class = parent.find('input[type="checkbox"]:checked').parents('li');
            selected_length = selected_class.length;

            //Disable or enable add or remove button
            if (selected_length > 0)
                parent.next('a.btn').removeClass('disabled');
            else
                parent.next('a.btn').addClass('disabled')
        });

        $('#class-activity ul.class-items+a.btn').on('click', function (event) {
            event.preventDefault();

            var class_container = $(this).prev('.class-items'),
                target_ul = $($(this).attr('data-target')),
                selected_class = class_container.find('input[type="checkbox"]:checked').parents('li');
            selected_length = selected_class.length;

            //Remove checked attr from selected class
            if (selected_class.find('.checker span.checked').length > 0) {
                selected_class.find('.checker span.checked').removeClass('checked')
                selected_class.find('input[type="checkbox"]').removeAttr('checked')
            } else {
                selected_class.find('input[type="checkbox"]').removeAttr('checked')
            }

            //Return if target element not found
            if (target_ul.length <= 0) return

            //Append to target element
            selected_class.appendTo(target_ul);
        })
    }

    return {
        organization_menu_nested: function () {
            _organization_menu_nested()
        },

        toggle_button_label: function () {
            _toggle_button_label()
        },
        addremove_activity_class: function () {
            add_remove_activity_class();
        }
    }
}();


var UIDatePickerRepon = function () {
    var handleDatePickers = function () {
        $(".ui_date_picker").datepicker();
        $(".ui_date_picker+.add-on").click(function () {
            $(this).prev('input.medium').datepicker("show");
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleDatePickers();
        }
    };
}();

var dataTableManage = function () {
    if (!jQuery().dataTable) {
        return;
    }

    var dataTableR = function () {
        $('table.table.dataTable').dataTable({
            "bSort": false,
            "sPaginationType": "bootstrap",
            "sDom": 'tp',
        });

        $('#modalReadReport table.table.read-report-table').dataTable({
            "bSort": false,
            "sPaginationType": "bootstrap",
            "sDom": 'ft<"pagination-center"p>',
        });

        $('table#blms-exam-search').dataTable({
            "sPaginationType": "bootstrap",
            "sDom": 'tp',
        });
    }

    return {
        //Active Datatable object
        init: function () {
            dataTableR();
        }
    };
}();


function strech_down_form_button(wizardContent) {

    if (wizardContent.length <= 0) return;

    //Get Current Wizard
    var currentWizard = wizardContent.parents('.streches-down-tab');

    var totalTab = currentWizard.find('.tab-downwards').length;
    var activeTab = currentWizard.find('.tab-downwards.active').length;

    if (activeTab > 1) currentWizard.find('.form-actions .btn.button-previous').css('display', 'inline-block')
}

$('document').ready(function () {

    $('.tab-downwards #oturum-sayisi-text').keyup(function (event) {

        if (!(event.keyCode == 8 || event.keyCode == 46 || (event.keyCode >= 35 && event.keyCode <= 40) || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105)))
            event.preventDefault();

        //Get Current Wizard
        var currentWizard = $(this).parents('.streches-down-tab');

        //get tar tab element
        var target_tab = $(this).attr('data-target');

        //return if target tab not found
        if (target_tab.length <= 0) return;

        if ($(this).val() < 6 && $(this).val() > 0) {
            //get this input value
            var this_value = $(this).val();
        }

        if (this_value > 0) {
            var new_tab_button = '';
            var new_tab_pane = '';
            for (var i = 1; i <= this_value; i++) {
                if (i == 1) {
                    new_tab_button += '<li class="active"><a data-toggle="tab" href="#oturum-tab' + i + '">Oturum ' + i + '</a></li>';
                    new_tab_pane += '<div id="oturum-tab' + i + '" class="active"></div>';
                } else {
                    new_tab_button += '<li><a data-toggle="tab" href="#oturum-tab' + i + '">Oturum ' + i + '</a></li>';
                    new_tab_pane += '<div id="oturum-tab' + i + '"></div>';
                }
            }
            $(target_tab).find('#oturum-tab ul.nav.nav-tabs').html(new_tab_button)
            $(target_tab).find('#oturum-tab .tab-content').html(new_tab_pane)
        }
        strech_down_form_button($(this));
    })

    $('.tab-downwards select.show-table').change(function () {
        //get target tab element
        var targetTab = $(this).attr('data-target');

        //Return if data-target value not found
        if (targetTab.length <= 0) return;

        //set default value to false
        var checkValue = '';

        //set value to checkValue variable if user input data 
        $('select.show-table').each(function () {
            if ($(this).val() != null) checkValue += $(this).val();
        })

        //Slide up or down after checking checkValue variable
        if (checkValue.length > 0) {
            $(targetTab).slideDown().addClass('active');
        } else {
            $(targetTab).slideUp().removeClass('active');
        }

        strech_down_form_button($(this));
    })


    $('.tab-downwards #oturum-sayisi-text').trigger('keyup');
    $('.tab-downwards select.show-table').trigger('change');

    $('.tab-downwards .btntable-ekleme-wizard a.btn.all-question').click(function (event) {
        //get target tab element
        var targetTab = $(this).attr('data-target');

        //Return if data-target value not found
        if (targetTab.length <= 0) return;

        //Prevent navigate webpage
        event.preventDefault()

        //Slide down target tab
        $(targetTab).slideDown().addClass('active');
        strech_down_form_button($(this));
    })

    $('.tab-downwards .btntable-ekleme-wizard a.btn').click(function (event) {
        event.preventDefault()
        //get target tab element
        var targetTab = $(this).attr('data-target');

        //Return if data-target value not found
        if (targetTab.length <= 0) return;

        //set default value to false
        var selectedinput = false;
        $('table.table.session-questions-list tr').each(function () {
            //get first input object
            var inputStates = $(this).find('td').eq(0).find('input');

            //set selectedInput true if user check any question
            if (inputStates.is(':checked')) selectedinput = true
        })

        //If not selected any checkbox then slide up target tab
        if (!selectedinput) {
            targetTab.slideUp();
            return;
        }

        //Slide down target tab
        $(targetTab).slideDown();
    })


    $('.tab-button-box a.btn').click(function (event) {
        event.preventDefault();
        $(this).toggleClass('active green');
        var tabName = $('.tab-button-box.subbtn a.btn.active').attr('data-target');
        var konularaBtn = $('.tab-button-box a.btn#konulara-gore.active');
        var zorlukBtn = $('.tab-button-box a.btn#zorluk-gore.active');
        if (konularaBtn.length > 0 && zorlukBtn.length > 0) tabName = $('#sub-tab3')
        $('.subtab-content').hide()
        $(tabName).toggle();
    });

    $('.streches-down-tab .form-actions a.btn.button-previous').click(function (event) {
        var currentWizard = $(this).parents('.streches-down-tab');

        var last_active_tab = currentWizard.find('.tab-downwards.active').length;

        currentWizard.find('.tab-downwards').eq(last_active_tab - 1).slideUp().removeClass('active');

        //tab-downwards
        event.preventDefault();
        strech_down_form_button($(this));
    });

    $('.streches-down-tab .form-actions a.btn.button-next').click(function (event) {
        var currentWizard = $(this).parents('.streches-down-tab');

        //get next tab object
        var next_tab = currentWizard.find('.tab-downwards.active').next('.tab-downwards');

        //return if next tab not found
        if (next_tab.length <= 0) return;

        //Show next tab
        next_tab.slideDown().addClass('active');

        //tab-downwards
        event.preventDefault();

        strech_down_form_button($(this));
    });

    //Show button after user clikced a button
    $('.dynamic-btn a.btn').click(function (event) {
        //get target element
        var targentElement = $(this).attr('data-target');
        if (targentElement.length <= 0) return;

        //add or remove 'show' css class
        $(targentElement).toggleClass('show');

        //prevent navigation
        event.preventDefault();
    })

    $('table.table.table-faq a.faq-btn').click(function (event) {
        //PREVENT CLICK EVENT
        event.preventDefault();

        //GET FAQ DETAILS ELEMENT
        var faqDetails = $(this).next('div.faq-details');

        //RETURN IF FAQ DETAILS ELEMENT NOT FOUND
        if (faqDetails.length <= 0) return;

        //GET FAQ DETAILS IMG ELEMENT
        var faqImage = faqDetails.find('img');

        //RETURN IF IMG FOUND SHOW ALERT BOX
        if (!faqDetails.is(':visible') && faqImage.length > 0)
            alert('The question text includes image.');

        //SLIDE TOGGLE DETAILS SECTION
        faqDetails.slideToggle('fast');
    });

    $('.gradebook-item-wizard .gradebook-inputs .input-label').live('click', function () {
        //Get the parent container
        var parent_container = $(this).parents('.pair-item');

        //hide current element
        $(this).hide();

        //show input element
        parent_container.find('input.m-wrap').show();
    })

    $('.tab-pane .pair-item input.activity-name').live('blur', function (event) {
        //Get the parent container
        var parent_container = $(this).parents('.pair-item');

        //get activity name
        var activity_name = $(this).val();

        //Hide this activity name
        $(this).hide();

        //show input element
        parent_container.find('.input-label').show();
    });

    $('.tab-pane .pair-item input.activity-weight').live('blur', function (event) {
        //Get the parent container
        var parent_container = $(this).parents('.pair-item');

        //get new value
        var new_value = parseInt($(this).val());
        new_value = new_value > 0 ? new_value : 0;

        //Hide this activity weight
        $(this).hide();

        //show input label
        parent_container.find('.input-label').text('%' + new_value).show();

        //get input container
        var get_input_container = $(this).parents('.activity-info').find('.gradebook-inputs');

        //Declare a activity weight variable to 0
        var activity_weight = 0;

        //get all activity name & it's value
        get_input_container.each(function () {
            var activity_weight_value = parseInt($(this).find('input.activity-weight').val());
            activity_weight_value = activity_weight_value >= 0 ? activity_weight_value : 0;
            $(this).find('input.activity-weight').val(activity_weight_value)
            activity_weight += activity_weight_value;
        });

        //show total activity value
        $(this).parents('.activity-info').next('.activity-label.activity-footer').children('.total-weight').text('%' + activity_weight);

        //get percentage container
        var percent_container = $(this).parents('.activity-item').find('.drop-percent');

        //get input value
        var input_value = parseInt($(this).val().replace('%', ''))

        //declare variable for percentage value
        var percentage_value = input_value >= 0 ? (input_value / 10) : 0;

        //print percentage value if container found
        if (percent_container.length > 0) percent_container.text('%' + percentage_value)
    });

    $('.tab-pane.step-input .btn.new-gradeitem').click(function (event) {
        event.preventDefault();

        var new_input = '<dl class="gradebook-inputs"><dt><label class="pair-item">';
        new_input += '<input type="text" class="m-wrap full-width activity-name" value="" />';
        new_input += '<div class="input-label"></div></label></dt>';
        new_input += '<dd><label class="pair-item">';
        new_input += '<input type="text" class="m-wrap full-width activity-weight" value="" data-type="number" data-max="100" />';
        new_input += '<div class="input-label"></div></label></dd></dl>';

        $(this).parents('.tab-pane.step-input').find('.activity-info').append(new_input);
    })

    $('ul#drag-items li').draggable({
        snap: ".gradebook-item-wizard .gradebook-inputs",
        helper: 'clone',
    });

    $('.gradebook-item-wizard').droppable({
        accept: "ul#drag-items li",
        drop: custom_drop
    });


    function custom_drop(event, ui) {
        //get parent container
        var droppable_area = $(this).find('.items-container .gradebook-inputs');

        droppable_area.droppable({
            accept: "ul#drag-items li",
            drop: function (event, ui) {
                //get acitivity name
                var activity_name = ui.draggable.text();

                var new_html = '<div class="activity-item">';
                new_html += '<div class="activity-name">' + activity_name + '</div>';
                new_html += '<label class="pair-item">';
                new_html += '<input type="text" class="m-wrap mini-width activity-weight" value="" data-type="number" data-max="100" />';
                new_html += '<span class="input-label"></span>';
                new_html += '</label>';
                new_html += '<span class="drop-percent"></span>';
                new_html += '</div><!-- END ACTIVITY ITEM -->';

                //get button
                var add_new_class_btn = $(this).find('.btn.add-class');
                if (add_new_class_btn.length > 0)
                    add_new_class_btn.before(new_html)
                else
                    $(this).find('.drop-items').append(new_html);
                $(ui.draggable).remove()
            }
        })
    }

    $('.preview-value .drop-items .btn.add-class ').live('click', function (event) {
        event.preventDefault();

        //New HTML for add new class
        var new_html = '<div class="activity-item">';
        new_html += '<div class="activity-name"><input type="text" class="m-wrap full-width" value="" /></div>';
        new_html += '<label class="pair-item">';
        new_html += '<input type="text" class="m-wrap mini-width activity-weight" value="" data-type="number" data-max="100" />';
        new_html += '<div class="m-wrap mini-width input-label"></div>';
        new_html += '</label>';
        new_html += '<span class="drop-percent"></span>';
        new_html += '</div><!-- END ACTIVITY ITEM -->';

        $(this).before(new_html)
    })

    $('.gradebook-item-wizard .form-actions a.btn.button-next').click(function (event) {
        event.preventDefault()

        //get input container
        var get_input_container = $('.step-input.tab-pane .gradebook-inputs');

        //Declare a activity name variable
        var activity_html = '';

        //get all activity name & it's value
        get_input_container.each(function (index) {

            //get activity name
            var activity_name = $(this).find('input.activity-name');

            //if user fill activity name then add them to next tab
            if (activity_name.val().length > 0) {
                //get activity name value
                var activity_name_value = $(this).find('input.activity-name').val();

                //get activity weight value
                var activity_weight = parseInt($(this).find('input.activity-weight').val());
                activity_weight = activity_weight >= 0 ? activity_weight : 0;

                //activity html for next tab
                activity_html += '<dl class="gradebook-inputs">';
                activity_html += '<dt>' + activity_name_value;
                activity_html += '<label class="pair-item">';
                activity_html += '<input type="text" class="m-wrap mini-width activity-weight" value="' + activity_weight + '" data-type="number" data-max="100" />';
                activity_html += '<div class="input-label">%' + activity_weight + '</div></label></dt>';
                activity_html += '<dd class="drop-items">';
                activity_html += '<a href="#" class="btn blue add-class"><i class="icon-plus"></i> Add Manually</a>';
                activity_html += '</dd></dl>';
            }
        });

        $('.preview-value .drag-item .items-container').html(activity_html)
    })

    function student_list_selected_user(current_table) {

        //return if no table found
        if (current_table.length <= 0) return

        if (current_table.hasClass('class-list')) return

        //get total selected check
        var total_checked = current_table.find('tbody tr td input.group-checked:checked').length;

        //get target label element
        var target_label = current_table.attr('data-label');

        //if found then print the total value
        if ($(target_label).length > 0) $(target_label).text(total_checked)
    }

    $('table.table input.group-checked').live('change', function () {
        //get current table
        var current_table = $(this).parents('table.table');

        if ($(this).parents('thead').length > 0) {
            //set default state to false
            var this_state = false;
            if ($(this).is(':checked')) this_state = true;

            //get column no
            var column_no = $(this).parents('th').index();

            current_table.find('tbody tr').each(function () {
                var current_check_box = $(this).find('td input.group-checked');

                current_check_box.attr('checked', this_state);

                if (current_check_box.parent('span').length > 0) {
                    current_check_box.parent('span').removeClass('checked');
                    if (this_state) current_check_box.parent('span').addClass('checked')
                }
            })
        }

        //run student_list_selected_user function
        student_list_selected_user(current_table);
    });

    $('#create-class .add-remove-student').live('click', function (event) {
        event.preventDefault();

        //set button type
        var add_button = true;
        if ($(this).hasClass('remove')) add_button = false

        //get student table
        var working_table = $('table#student-list');
        if (!add_button) working_table = $('#class-list table.class-list');

        //get selected row
        var selected_rows = working_table.find('tbody td input.group-checked:checked').parents('tr')

        //Uncheck selected input (plugins)
        if (selected_rows.find('span.checked').length > 0)
            selected_rows.find('span.checked').removeClass('checked')

        //Uncheck selected input
        selected_rows.find('input.group-checked').attr('checked', false)

        if (selected_rows.length <= 0) {
            alert('No row selected. Please select table row.');
            return;
        }

        //get target table
        var target_table = $('#class-list table.class-list');
        if (!add_button) target_table = $('table#student-list');

        //clone selected student into new table
        selected_rows.clone(target_table.children('tbody').append(selected_rows))

        //get selected student table container
        var table_container = $('#class-list');

        //get current user in class list
        var total_user_in_class = table_container.find('table.class-list tbody tr').length;

        if (total_user_in_class > 0) {
            //hide "Class doesn’t have any users yet." text
            table_container.find('.no-student').hide();

            //show table with student list
            table_container.find('.selected-student').removeClass('hide');

            //get total selected user in class list
            var total_checked = table_container.find('table.class-list tbody tr').length;

            //get target label element
            var target_label = table_container.find('table.class-list').attr('data-label');

            //show total user in class list
            $(target_label).text(total_checked)
        } else {
            //show "Class doesn’t have any users yet." text
            table_container.find('.no-student').show();

            //hide table with student list
            table_container.find('.selected-student').addClass('hide');
        }

        var class_tables = $('table#student-list, table.class-list')

        //Uncheck selected input (plugins)
        if (class_tables.find('span.checked').length > 0)
            class_tables.find('span.checked').removeClass('checked')

        //Uncheck selected input
        class_tables.find('input.group-checked').attr('checked', false)

        //run student_list_selected_user function
        student_list_selected_user($('table#student-list'));
    })

    //set global variable default value to false
    var email_edit = false;
    $('#email-editor textarea').live('keydown', function () {
        //change email_edit value to true if user edited the email template
        email_edit = true;
    })

    var selected_template = '';
    var selected_template_name = '';
    var new_template = '';
    var new_template_name = '';

    $('#edit-email-template select#choose-template').change(function () {
        //get the seleccted template
        new_template = $(this).find('option:selected').attr('data-template')
        new_template_name = $(this).find('option:selected').text();

        //return if selected template not found
        if (new_template.length <= 0) return

        //no event occur if user select the same template
        if (selected_template == new_template) return;

        if (!email_edit) {
            //run new_email_template function
            new_email_template();
            return;
        }

        $('#save-email-templete q.template-name').text(selected_template_name);
        $('#save-email-templete').modal('show');
    });

    $('#save-email-templete .btn.save').click(function () {
        var new_code = $('#email-editor textarea').val();

        $(selected_template).text(new_code);
        new_email_template()
    })

    $('#save-email-templete .btn.no-save').click(function () {
        new_email_template()
    })

    function new_email_template() {
        selected_template = new_template;
        selected_template_name = new_template_name;

        $('#edit-email-template .template-edit-area .edit-title').text(new_template_name + ' HTML Source Code');
        //slide down template editor screen
        //$('#edit-email-template #email-editor').empty().html( $(new_template).html() );

        $('#edit-email-template #email-editor').html('<textarea class="m-wrap span12 email-editor" name="email-template">' + $(new_template).html() + '</textarea>');
        $('#edit-email-template .template-edit-area').slideDown();
        //set email edit false
        email_edit = false;
    }

    $('#email-tools .btn.preview-email').click(function (event) {
        event.preventDefault();
        var email_valid = true;

        if (!email_valid) {
            $('#email-not-valid').modal('show');
        } else {
            var html_code = $('#email-editor textarea').val()
            var preview_frame = document.createElement("iframe");
            $('#preview-email .modal-body').html(preview_frame);
            var new_template = preview_frame.contentDocument;
            new_template.open();
            new_template.write(html_code);
            new_template.close();
            $('#preview-email').modal('show');
        }

    })
});